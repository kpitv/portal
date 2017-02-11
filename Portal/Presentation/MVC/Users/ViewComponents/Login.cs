using Microsoft.AspNetCore.Mvc;
using Portal.Presentation.Identity.Services;

namespace Portal.Presentation.MVC.Users.ViewComponents
{
    public class LogIn : ViewComponent
    {
        private readonly IIdentityManager manager;
        public LogIn(IIdentityManager manager)
        {
            this.manager = manager;
        }
        public IViewComponentResult Invoke() =>
            View(manager.SignIn.IsSignedIn(HttpContext.User));
    }
}
