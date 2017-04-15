using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Portal.Application.Interfaces;
using Portal.Presentation.Identity.Data;
using Portal.Presentation.Identity.Users.Models;

namespace Portal.Presentation.Identity.Users
{
    public class IdentityManager
    {
        private readonly IdentityDatabaseService databaseService;
        private readonly IEmailService emailService;

        public UserManager<User> User { get; }
        public SignInManager<User> SignIn { get; }
        public RoleManager<IdentityRole> Role { get; }

        public IdentityManager(IdentityDatabaseService databaseService, IEmailService emailService,
            UserManager<User> user, SignInManager<User> signIn,
            RoleManager<IdentityRole> role)
        {
            this.databaseService = databaseService;
            this.emailService = emailService;
            User = user;
            SignIn = signIn;
            Role = role;
        }

        public void InitializeUsersAsync()
        {
            databaseService.Database.EnsureCreated();

            if (databaseService.Users.Any()) return;

            var users = new List<User>()
            {
                new User("grandepianisto"),
                new User("stasphere"),
                new User("shoolz")
            };

            users.ForEach(u => User.CreateAsync(u, "12345678"));
        }

        public void InviteUser(string email)
        {
            ClearExpiredEmailTokens();

            string token = GenerateToken();
            
            if (GetEmailToken(email) != null)
                RemoveEmailToken(email);

            databaseService.EmailTokens.Add(new EmailToken
            {
                Email = email,
                TokenHash = token.GetHashCode().ToString(),
                Created = DateTime.Now
            });
            databaseService.SaveChanges();

            string link = $"https://localhost:44393/users/create/{token}";
            string message = $"Привет, переходи по <a href=\"{link}\">ссылке</a> и регистрируйся!";
            emailService.SendEmailAsync((email, "Регистрация", message));
        }

        public string GetEmail(string token) =>
            databaseService.EmailTokens
                .Where(e => e.TokenHash == token.GetHashCode().ToString())
                .Select(e => e.Email).FirstOrDefault();

        private EmailToken GetEmailToken(string email) =>
            databaseService.EmailTokens
                .FirstOrDefault(e => string.Equals(e.Email, email, StringComparison.CurrentCultureIgnoreCase));

        private void ClearExpiredEmailTokens()
        {
            databaseService.RemoveRange(databaseService.EmailTokens
                .Where(e => e.Created.AddHours(24) < DateTime.Now).ToList());
            databaseService.SaveChanges();
        }

        public void RemoveEmailToken(string email)
        {
            databaseService.EmailTokens.Remove(GetEmailToken(email));
            databaseService.SaveChanges();
        }

        private static string GenerateToken()
        {
            var random = new Random();
            string token = string.Empty;
            for (var i = 0; i < 17; i++)
                token += (char)random.Next(97, 122);
            return token;
        }

        public bool VerifyEmail(string email) =>
            !databaseService.Users.Any(u => string.Equals(u.Email, email, StringComparison.CurrentCultureIgnoreCase));

        public bool VerifyUsername(string username) =>
            !databaseService.Users.Any(u => string.Equals(u.UserName, username, StringComparison.CurrentCultureIgnoreCase));
    }
}
