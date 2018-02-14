using IdentityServer4;
using System.Collections.Generic;

namespace AuthorizationServer.Services.Clients
{
    public partial class ClientService
    {
        private List<string> AllowedScopes = new List<string>
        {
             IdentityServerConstants.StandardScopes.OpenId,
             IdentityServerConstants.StandardScopes.Profile,
             IdentityServerConstants.StandardScopes.Email
        };
    }
}
