using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Presentation.Identity.Services;

namespace Portal.Presentation.MVC.Users
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IIdentityManager manager;
        public UsersController(IIdentityManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string userName, string password, string isPersistent)
        {
            await manager.SignIn.PasswordSignInAsync(userName, password, isPersistent == "on", false);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await manager.SignIn.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}