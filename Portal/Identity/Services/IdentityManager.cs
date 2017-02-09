using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Portal.Presentation.Identity.Data;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Presentation.Identity.Services
{
    public class IdentityManager : IIdentityManager
    {
        readonly IdentityDatabaseService service;
        public UserManager<IdentityUser> User { get; private set; }
        public SignInManager<IdentityUser> SignIn { get; private set; }
        public RoleManager<IdentityRole> Role { get; private set; }

        public IdentityManager(IdentityDatabaseService service,
            UserManager<IdentityUser> User, SignInManager<IdentityUser> SignIn, RoleManager<IdentityRole> Role)
        {
            this.service = service;
            this.User = User;
            this.SignIn = SignIn;
            this.Role = Role;
        }

        public void InitializeUsers()
        {
            service.Database.EnsureCreated();

            if (service.Users.Any())
            {
                return;
            }

            var users = new List<IdentityUser>()
            {
                new IdentityUser("grandepianisto"),
                new IdentityUser("stasphere"),
                new IdentityUser("shoolz")
            };

            users.ForEach(u => User.CreateAsync(u, "12345678"));
        }
    }
}
