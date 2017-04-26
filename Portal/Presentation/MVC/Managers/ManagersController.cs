using Microsoft.AspNetCore.Mvc;
using Portal.Presentation.Identity.Users;
using Portal.Presentation.MVC.Managers.ViewModels;

namespace Presentation.MVC.Managers
{
    public class ManagersController : Controller
    {
        private readonly IdentityManager manager;

        public ManagersController(IdentityManager manager)
        {
            this.manager = manager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("InviteUser");
        }

        [HttpGet]
        public IActionResult InviteUser()
        {
            return View(new InviteUserViewModel());
        }

        [HttpPost]
        public IActionResult InviteUser(InviteUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            manager.InviteUser(model.Email);

            model.Message = "Invitation has been sent";

            return View(model);
        }
    }
}