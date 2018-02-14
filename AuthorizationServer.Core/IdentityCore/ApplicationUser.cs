using Microsoft.AspNetCore.Identity;
using System;

namespace AuthorizationServer.Core.IdentityCore
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
    }
}
