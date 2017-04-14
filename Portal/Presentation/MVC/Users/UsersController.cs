using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Presentation.Identity.Users;

namespace Portal.Presentation.MVC.Users
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IdentityManager manager;

        public UsersController(IdentityManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public IActionResult Create(string token)
        {
            string email = manager.GetEmail(token);
            if (string.IsNullOrEmpty(email))
                return NotFound();
            return View();
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