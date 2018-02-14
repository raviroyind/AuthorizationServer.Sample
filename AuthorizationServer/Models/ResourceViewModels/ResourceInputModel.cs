using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models.ResourceViewModels
{
    public class ResourceInputModel
    {
        public string ClientId { get; set; }
        public string ApiName { get; set; }
    }
}
