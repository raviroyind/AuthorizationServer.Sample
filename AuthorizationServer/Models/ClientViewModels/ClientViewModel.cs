using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models.ClientViewModels
{
    public class ClientViewModel
    {
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool RequireConsent { get; set; }
        public string RedirectUrl { get; set; }
        public string PostLogoutRedirectUrl { get; set; }
        public string AllowedScope { get; set; }
    }
}
