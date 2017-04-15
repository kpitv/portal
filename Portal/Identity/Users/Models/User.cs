using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Portal.Presentation.Identity.Users.Models
{
    public class User : IdentityUser
    {
        public User(string username) : base(username) { }
        public User() { }
        public string Language { get; set; } = "ru";
    }

}
