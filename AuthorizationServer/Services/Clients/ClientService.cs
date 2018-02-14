using AuthorizationServer.Core.Domain;
using AuthorizationServer.Core.Repository;
using AuthorizationServer.Models.ClientViewModels;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Services.Clients
{
    public partial class ClientService : IClientService
    {
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly ConfigurationDbContext _configurationDbContext;
        private readonly IRepository<UserXClient> _userXClientRepository;
        private readonly IRepository<ClientRedirectUri> _clientRedirectUriRepository;
        private readonly IRepository<ClientScope> _clientScopeRepository;

        public ClientService(IClientStore clientStore,
            IResourceStore resourceStore,
            ConfigurationDbContext configurationDbContext,
            IRepository<UserXClient> userXClientRepository,
            IRepository<ClientRedirectUri> clientRedirectUriRepository,
            IRepository<ClientScope> clientScopeRepository)
        {
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _configurationDbContext = configurationDbContext;
            _userXClientRepository = userXClientRepository;
            _clientRedirectUriRepository = clientRedirectUriRepository;
            _clientScopeRepository = clientScopeRepository;
        }

        public async Task<(bool Succeeded, string ErrorMsg)> AddAsync(string userId, string clientName, string redirectUrl, string postLogoutRedirectUrl, bool requireConsent)
        {
            bool succeeded = false;

            string errorMsg = string.Empty;

            //1. 判断客户端名称是否已存在。
            var hasClients = _configurationDbContext.Clients.SingleOrDefault(x => x.ClientName == clientName) != null;

            if (!hasClients)
            {
                //2. 添加该客户端。
                var clientId = Guid.NewGuid().ToString();
                var ClientSecret = Guid.NewGuid().ToString();

                var client = new Client
                {
                    ClientId = Guid.NewGuid().ToString(),
                    ClientName = clientName,
                    ClientSecrets =
                    {
                        new Secret
                        {
                            Description = ClientSecret,
                            Value = ClientSecret.Sha256()
                        }
                    },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = { redirectUrl },
                    PostLogoutRedirectUris = { postLogoutRedirectUrl },
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = requireConsent,
                    AllowedScopes = AllowedScopes,
                };

                await _configurationDbContext.Clients.AddAsync(client.ToEntity());

                await _configurationDbContext.SaveChangesAsync();

                //3. 添加用户以及客户端的信息。
                var userXClient = new UserXClient
                {
                    ClientId = client.ClientId,
                    UserId = userId,
                    Status = 0
                };

                await _userXClientRepository.InsertAsync(userXClient);

                succeeded = true;
            }
            else
            {
                errorMsg = "该名称已存在";
            }

            return (succeeded, errorMsg);
        }

        public async Task<(bool Succeeded, string ErrorMsg)> UpdateAsync(string userId, string clientId, string clientName, string redirectUrl, string postLogoutRedirectUrl, bool requireConsent, string allowedScope)
        {
            bool succeeded = false;

            string errorMsg = string.Empty;

            var userXClient = await _userXClientRepository.SingleOrDefaultAsync(x => x.UserId == userId && x.ClientId == clientId);

            if (userXClient != null)
            {
                var client = _configurationDbContext.Clients.SingleOrDefault(x => x.ClientId == clientId);

                if (client != null)
                {
                    //update redirectUri
                    var redirectUri = await _clientRedirectUriRepository.SingleOrDefaultAsync(x => x.ClientId == client.Id);

                    if (redirectUri != null)
                    {
                        redirectUri.RedirectUri = redirectUrl;

                        await _clientRedirectUriRepository.UpdateAsync(redirectUri);
                    }

                    //update allowedScope
                    var scope = await _clientScopeRepository.SingleOrDefaultAsync(x => !AllowedScopes.Contains(x.Scope) && x.ClientId == client.Id);

                    if (scope == null)
                    {
                        scope = new ClientScope
                        {
                            ClientId = client.Id,
                            Scope = allowedScope
                        };

                        await _clientScopeRepository.InsertAsync(scope);
                    }
                    else
                    {
                        scope.Scope = allowedScope;

                        await _clientScopeRepository.UpdateAsync(scope);
                    }

                    //update client
                    client.ClientName = clientName;
                    client.RequireConsent = requireConsent;

                    _configurationDbContext.Clients.Update(client);

                    await _configurationDbContext.SaveChangesAsync();

                    succeeded = true;
                }
            }
            else
            {
                errorMsg = "你没有权限更新";
            }

            return (succeeded, errorMsg);
        }

        public async Task<List<ClientViewModel>> GetByUserIdAsync(string userId)
        {
            List<ClientViewModel> result = null;

            var userXClients = await _userXClientRepository.ToListAsync(x => x.UserId == userId);

            if (userXClients?.Count > 0)
            {
                var clientIds = userXClients.Select(x => x.ClientId).ToList();

                var clients = _configurationDbContext.Clients.Where(x => clientIds.Contains(x.ClientId)).ToList();

                if (clients?.Count > 0)
                {
                    result = new List<ClientViewModel>();

                    foreach (var item in clients)
                    {
                        var client = await _clientStore.FindClientByIdAsync(item.ClientId);

                        result.Add(new ClientViewModel
                        {
                            ClientId = client.ClientId,
                            ClientName = client.ClientName,
                            ClientSecret = client.ClientSecrets.FirstOrDefault().Description,
                            PostLogoutRedirectUrl = client.PostLogoutRedirectUris.FirstOrDefault(),
                            RedirectUrl = client.RedirectUris.FirstOrDefault(),
                            RequireConsent = client.RequireConsent,
                            AllowedScope = client.AllowedScopes.Where(x => !AllowedScopes.Contains(x)).SingleOrDefault()
                        });
                    }
                }
            }

            return result;
        }

        public async Task<ClientInputModel> GetByIdAsync(string userId, string clientId)
        {
            var userXClient = await _userXClientRepository.SingleOrDefaultAsync(x => x.UserId == userId && x.ClientId == clientId);

            if (userXClient != null)
            {
                var client = await _clientStore.FindClientByIdAsync(userXClient.ClientId);

                if (client != null)
                {
                    var model = new ClientInputModel
                    {
                        ClientId = client.ClientId,
                        ClientName = client.ClientName,
                        ClientSecret = client.ClientSecrets.FirstOrDefault().Description,
                        PostLogoutRedirectUrl = client.PostLogoutRedirectUris.FirstOrDefault(),
                        RedirectUrl = client.RedirectUris.FirstOrDefault(),
                        RequireConsent = client.RequireConsent
                    };

                    return model;
                }
            }

            return null;
        }
    }
}

