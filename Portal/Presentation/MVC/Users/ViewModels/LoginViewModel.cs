using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Presentation.MVC.Users.ViewModels
{
    public class LoginViewModel
    {
        public bool IsLogedIn { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
    }
}
