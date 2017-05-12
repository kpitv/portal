using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Presentation.Identity.Users;
using Portal.Presentation.Identity.Users.Models;
using Portal.Presentation.MVC.Home;
using Portal.Presentation.MVC.Users.ViewModels;

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
        [AllowAnonymous]
        [Route("[controller]/[action]/{token}")]
        public IActionResult Create(string token)
        {
            int i = token.GetHashCode();
            string email = manager.GetEmail(token);
            if (string.IsNullOrEmpty(email))
                return NotFound();
            return View(new CreateViewModel
            {
                Email = email
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = new User
            {
                Language = model.Language,
                UserName = model.Username,
                Email = model.Email
            };
            await manager.User.CreateAsync(user, model.Password);
            await manager.SignIn.SignInAsync(user, isPersistent: false);
            manager.RemoveEmailToken(model.Email);
            return RedirectToAction("Create", "Members");
        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var user = await manager.User.GetUserAsync(HttpContext.User);
            return View(new ChangePasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Settings");

            var user = await manager.User.GetUserAsync(HttpContext.User);

            if (!string.Equals(model.Username, user.UserName, StringComparison.CurrentCultureIgnoreCase))
                await manager.User.SetUserNameAsync(user, model.Username);

            return RedirectToAction("Settings");

        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Settings");
            var user = await manager.User.GetUserAsync(HttpContext.User);

            var result = await manager.User.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
                return RedirectToAction("Settings");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View("Settings");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string userName, string password, string isPersistent)
        {
            await manager.SignIn.PasswordSignInAsync(userName, password, isPersistent == "on", false);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await manager.SignIn.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyEmail(string email) =>
           manager.VerifyEmail(email) ? Json(true) : Json("Error: email exists");

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyUsername(string username) =>
           manager.VerifyEmail(username) ? Json(true) : Json("Error: username exists");
    }
}