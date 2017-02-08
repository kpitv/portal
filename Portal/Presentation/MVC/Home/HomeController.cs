using Microsoft.AspNetCore.Mvc;
using Portal.Application;

namespace Presentation.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}