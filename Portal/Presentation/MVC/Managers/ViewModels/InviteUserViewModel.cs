using System.ComponentModel.DataAnnotations;

namespace Portal.Presentation.MVC.Managers.ViewModels
{
    public class InviteUserViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Message { get; set; }
    }
}
