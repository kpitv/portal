using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Presentation.MVC.Users.ViewModels
{
    public class ChangeUsernameViewModel
    {
        [Required]
        [Remote(nameof(UsersController.VerifyUsername), "Users")]
        public string Username { get; set; }
    }
}
