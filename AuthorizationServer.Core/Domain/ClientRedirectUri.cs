using AuthorizationServer.Core.MyCore;

namespace AuthorizationServer.Core.Domain
{
    public class ClientRedirectUri : EntityBase
    {
        public int ClientId { get; set; }
        public string RedirectUri { get; set; }
    }
}
