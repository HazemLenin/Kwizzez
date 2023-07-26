using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Data;
using Kwizzez.Domain.Constants;
using Kwizzez.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Kwizzez.DAL.Services.Seeds
{
    public class SeedData : ISeedData
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedData(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Seed()
        {
            await SeedRoles();
            await SeedAdmin();
        }

        private async Task SeedRoles()
        {
            var roles = Roles.GetRoles();

            using var roleManger = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in roles)
            {
                if (!await roleManger.RoleExistsAsync(role))
                    await roleManger.CreateAsync(new() { Name = role });
            }
        }

        private async Task SeedAdmin()
        {
            using var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var adminExists = userManager.Users.Any(u => userManager.IsInRoleAsync(u, Roles.Admin).Result);
            if (!adminExists)
            {
                var result = await userManager.CreateAsync(new()
                {
                    Email = "admin@example.com",
                    UserName = "admin",
                    FirstName = "admin first name",
                    LastName = "admin last name",
                    DateOfBirth = new(1999, 1, 1)
                });
                var admin = await userManager.FindByEmailAsync("admin@example.com");
                await userManager.AddPasswordAsync(admin, "Hello%world1");
            }
        }
    }
}
