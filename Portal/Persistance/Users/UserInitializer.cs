using Microsoft.AspNetCore.Identity;
using Portal.Domain.Users;
using Portal.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Persistance.Users
{
    public class UserInitializer
    {
        readonly UserManager<User> userManager;
        DatabaseService databaseService;

        public UserInitializer(UserManager<User> userManager, DatabaseService databaseService)
        {
            this.userManager = userManager;
            this.databaseService = databaseService;
        }

        public void Initialize()
        {
            databaseService.Database.EnsureCreated();

            if (databaseService.Users.Any())
            {
                return;
            }

            var users = new List<User>()
            {
                new User("grandepianisto"),
                new User("stasphere"),
                new User("shoolz")
            };

            foreach (var user in users)
            {
                var result = userManager.CreateAsync(user, "12345678");
            }

            databaseService.SaveChanges();
        }
    }
}
