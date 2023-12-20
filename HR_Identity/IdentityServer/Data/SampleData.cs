using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Data
{
    public class SampleData
    {
        private static Dictionary<string, ApplicationUser> _users = new Dictionary<string, ApplicationUser>()
        {
            {
                "Administrator",
                new ApplicationUser("admin")
            {
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),

            }
            },
            {
                "User",
                new ApplicationUser("user")
            {
                Email = "user@example.com",
                NormalizedEmail = "USER@EXAMPLE.COM",
                UserName = "user",
                NormalizedUserName = "USER",
                PhoneNumber = "+22222222222",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            }}
        };

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<AuthDbContext>();
            foreach (var user in _users)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == user.Key))
                {
                    context.Roles.Add(new IdentityRole() { Name = user.Key, NormalizedName = user.Key.ToUpper() });
                }
            }

            foreach (var user in _users)
            {
                if (!context.Users.Any(u => u.UserName == user.Value.UserName))
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user.Value, user.Value.GetRawPassword());
                    user.Value.PasswordHash = hashed;
                    var userStore = new UserStore<ApplicationUser>(context);
                    var result = userStore.CreateAsync(user.Value);
                }
            }

            var re = context.SaveChangesAsync().Result;
            foreach (var user in _users)
            {
                AssignRoles(serviceProvider, user.Value.UserName, new string[] { user.Key.ToUpper() });
            }
        }

        public static async void AssignRoles(IServiceProvider services, string login, string[] roles)
        {
            var _userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await _userManager.FindByNameAsync(login);
            var result = await _userManager.AddToRolesAsync(user, roles);
            return;
        }
    }
}
