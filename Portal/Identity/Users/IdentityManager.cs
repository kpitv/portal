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

        public UserManager<IdentityUser> User { get; }
        public SignInManager<IdentityUser> SignIn { get; }
        public RoleManager<IdentityRole> Role { get; }

        public IdentityManager(IdentityDatabaseService databaseService, IEmailService emailService,
            UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn,
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

            var users = new List<IdentityUser>()
            {
                new IdentityUser("grandepianisto"),
                new IdentityUser("stasphere"),
                new IdentityUser("shoolz")
            };

            users.ForEach(u => User.CreateAsync(u, "12345678"));
        }

        public void InviteUser(string email)
        {
            ClearExpiredEmailTokens();

            string token = GenerateToken();

            var emailToken = GetEmailToken(email);
            if (emailToken != null)
            {
                databaseService.EmailTokens.Remove(emailToken);
                databaseService.SaveChanges();
            }

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

        public EmailToken GetEmailToken(string email) =>
            databaseService.EmailTokens
                .Single(e => string.Equals(e.Email, email, StringComparison.CurrentCultureIgnoreCase));

        private void ClearExpiredEmailTokens()
        {
            databaseService.RemoveRange(databaseService.EmailTokens
                .Where(e => e.Created.AddHours(24) < DateTime.Now).ToList());
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
    }
}
