using System.Collections.Generic;

namespace Portal.Presentation.MVC.Users.ViewModels
{
    public class LoginViewModel
    {
        public bool IsLoggedIn { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public Dictionary<string,string> Errors { get; set; }
    }
}
