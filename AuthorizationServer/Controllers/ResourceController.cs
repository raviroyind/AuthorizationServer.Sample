using AuthorizationServer.Models.ResourceViewModels;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthorizationServer.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly ConfigurationDbContext _configurationDbContext;


        public ResourceController(IClientStore clientStore,
                                  IResourceStore resourceStore,
                                  ConfigurationDbContext configurationDbContext)
        {
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _configurationDbContext = configurationDbContext;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Apply(ResourceInputModel model)
        {
            var apiResources = await _resourceStore.FindApiResourceAsync(model.ApiName);

            if (apiResources != null)
            {
                var client = await _clientStore.FindClientByIdAsync(model.ClientId);

                if (client != null)
                {
                    if (!client.AllowedScopes.Contains(apiResources.Name))
                    {
                        client.AllowedScopes = client.AllowedScopes ?? new string[] { };
                        client.AllowedScopes.Add(apiResources.Name);

                        _configurationDbContext.Clients.Update(client.ToEntity());

                        await _configurationDbContext.SaveChangesAsync();
                    }
                }
            }

            return null;
        }
    }
}