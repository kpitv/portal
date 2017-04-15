using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Presentation.MVC.Users.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(nameof(UsersController.VerifyEmail), "Users")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        [Remote(nameof(UsersController.VerifyUsername), "Users")]
        public string Username { get; set; }

        [Required]
        public string Language { get; set; }
    }
}
