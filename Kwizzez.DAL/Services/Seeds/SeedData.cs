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
            using var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            using var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            using var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await SeedRoles(roleManager);
            await SeedAdmin(context, userManager);
            await SeedTeacher(context, userManager);
            await SeedStudent(context, userManager);
        }

        private async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = Roles.GetRoles();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new() { Name = role });
            }
        }

        private async Task SeedAdmin(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == Roles.Admin);
            var adminExists = context.UserRoles.Any(r => r.RoleId == adminRole.Id);
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
                await userManager.AddToRoleAsync(admin, Roles.Admin);
            }
        }

        private async Task SeedTeacher(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var teacherRole = context.Roles.FirstOrDefault(r => r.Name == Roles.Teacher);
            var teacherExists = context.UserRoles.Any(r => r.RoleId == teacherRole.Id);
            if (!teacherExists)
            {
                var result = await userManager.CreateAsync(new()
                {
                    Email = "teacher@example.com",
                    UserName = "teacher",
                    FirstName = "teacher first name",
                    LastName = "teacher last name",
                    DateOfBirth = new(1999, 1, 1)
                });
                var teacher = await userManager.FindByEmailAsync("teacher@example.com");
                await userManager.AddPasswordAsync(teacher, "Hello%world1");
                await userManager.AddToRoleAsync(teacher, Roles.Teacher);
            }
        }

        private async Task SeedStudent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var studentRole = context.Roles.FirstOrDefault(r => r.Name == Roles.Student);
            var studentExists = context.UserRoles.Any(r => r.RoleId == studentRole.Id);
            if (!studentExists)
            {
                var result = await userManager.CreateAsync(new()
                {
                    Email = "student@example.com",
                    UserName = "student",
                    FirstName = "student first name",
                    LastName = "student last name",
                    DateOfBirth = new(1999, 1, 1)
                });
                var student = await userManager.FindByEmailAsync("student@example.com");
                await userManager.AddPasswordAsync(student, "Hello%world1");
                await userManager.AddToRoleAsync(student, Roles.Student);
            }
        }
    }
}
