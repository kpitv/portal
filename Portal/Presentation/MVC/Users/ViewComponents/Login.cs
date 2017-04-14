using Microsoft.AspNetCore.Mvc;
using Portal.Presentation.Identity.Users;

namespace Portal.Presentation.MVC.Users.ViewComponents
{
    public class Login : ViewComponent
    {
        private readonly IdentityManager manager;
        public Login(IdentityManager manager)
        {
            this.manager = manager;
        }
        public IViewComponentResult Invoke() =>
            View(manager.SignIn.IsSignedIn(HttpContext.User));
    }
}
