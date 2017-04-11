using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Presentation.Identity.Services;
using System.Threading.Tasks;

namespace Presentation.MVC.Users
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
        public IActionResult Logout()
        {
            manager.SignIn.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}