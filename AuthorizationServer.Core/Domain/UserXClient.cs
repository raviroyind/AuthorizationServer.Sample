using AuthorizationServer.Core.MyCore;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Core.Domain
{
    public class UserXClient : EntityBase
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ClientId { get; set; }

        public int Status { get; set; }
    }
}
