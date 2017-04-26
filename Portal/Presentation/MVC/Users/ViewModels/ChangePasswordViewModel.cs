using System.ComponentModel.DataAnnotations;

namespace Portal.Presentation.MVC.Users.ViewModels
{
    public class ChangePasswordViewModel
    {
       
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string OldPassword { get; set; }
    }
}
