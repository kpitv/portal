using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Domain.Users;

namespace Portal.Presentation.MVC.Shared.Components
{
    public class Login : ViewComponent
    {
        SignInManager<User> signInManager;
        public Login(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }
        public IViewComponentResult Invoke() =>
            View(signInManager.IsSignedIn(HttpContext.User));
    }
}
