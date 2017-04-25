using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Portal.Presentation.MVC.Assets.ViewModels;

namespace Portal.Presentation.MVC.Assets
{
    public class AssetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateType(AssetTypeViewModel model)
        {
            return Ok();
        }
    }
}