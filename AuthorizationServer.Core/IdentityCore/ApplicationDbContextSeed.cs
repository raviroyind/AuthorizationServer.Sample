using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Core.IdentityCore
{
    public class ApplicationDbContextSeed
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public async Task SeedAsync(ApplicationDbContext context, IServiceProvider services)
        {
            if (!context.Roles.Any())
            {
                _roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                var role = new ApplicationRole
                {
                    Name = "Administrator",
                    NormalizedName = "Administrator",
                    Remark = "管理员"
                };

                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new Exception("初始默认角色失败");
                }
            }

            if (!context.Users.Any())
            {
                _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var defaultUser = new ApplicationUser
                {
                    UserName = "bruce",
                    Email = "bruce@qq.com",
                    NormalizedUserName = "bruce",
                    Address = "颐和610华定科技有限公司"
                };

                var result = await _userManager.CreateAsync(defaultUser, "8308911");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(defaultUser, "Administrator");
                else
                    throw new Exception("初始默认用户失败");
            }
        }
    }
}
