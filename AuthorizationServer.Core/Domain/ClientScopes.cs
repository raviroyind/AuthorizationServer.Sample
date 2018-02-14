using AuthorizationServer.Core.MyCore;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationServer.Core.Domain
{
    public class ClientScope : EntityBase
    {
        public int ClientId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Scope { get; set; }
    }
}
