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
        public IActionResult LogInAsync(string returnUrl)
        {
            return View("LogIn");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogInAsync(string userName, string password, string isPersistent)
        {
            await manager.SignIn.PasswordSignInAsync(userName, password, isPersistent == "on", false);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            manager.SignIn.SignOutAsync();
            return RedirectToAction(nameof(LogInAsync));
        }
    }
}