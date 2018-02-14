using Microsoft.AspNetCore.Identity;
using System;

namespace AuthorizationServer.Core.IdentityCore
{
    public class ApplicationRole : IdentityRole
    {
        public string Remark { get; set; }
    }
}
