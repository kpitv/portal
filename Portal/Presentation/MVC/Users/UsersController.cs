using Microsoft.AspNetCore.Mvc;
using Portal.Application.Users;
using System.Threading.Tasks;

namespace Presentation.MVC.Users
{
    public class UsersController : Controller
    {
        readonly IUsersQueryService query;
        public UsersController(IUsersQueryService query)
        {
            this.query = query;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogInAsync(string userName, string password, string isPersistent)
        {
            await query.SignInAsync(userName, password, true ? isPersistent == "on" : false);
            return RedirectToAction("Index", "Home");
        }
    }
}