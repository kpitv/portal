using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Portal.Presentation.MVC.Assets.ViewModels;
using AutoMapper;
using Portal.Application.Assets.Commands;
using Portal.Application.Assets.Commands.Models;
using Portal.Application.Assets.Queries;
using Portal.Application.Shared;

namespace Portal.Presentation.MVC.Assets
{
    public class AssetsController : Controller
    {
        private readonly IAssetTypeQueries assetTypeQueries;
        private readonly IAssetTypeCommands assetTypeCommands;
        private readonly IMapper mapper;

        public AssetsController(IAssetTypeQueries assetTypeQueries, IAssetTypeCommands assetTypeCommands, IMapper mapper)
        {
            this.assetTypeQueries = assetTypeQueries;
            this.assetTypeCommands = assetTypeCommands;
            this.mapper = mapper;
        }

        public IActionResult Index() =>
            View(assetTypeQueries.GetAssetTypes());

        [HttpGet]
        public IActionResult CreateType()
        {
            return View(new AssetTypeViewModel()
            {
                Properties = new List<string>()
            });
        }

        [HttpPost]
        public IActionResult CreateType(AssetTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_AssetTypeForm", model);

            var assetType = mapper.Map<CreateAssetTypeModel>(model);
            try
            {
                assetTypeCommands.Create(assetType);
            }
            catch (ApplicationException ex) when (ex.Type == ApplicationExceptionType.Validation)
            {
                model.Errors = ex.Errors;
                return PartialView("_AssetTypeForm", model);
            }
            catch (ApplicationException)
            {
                // redirect to error view
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Assets/EditType/{name}")]
        public IActionResult EditType(string name)
        {
            var assetType = assetTypeQueries.FindAssetTypes(a => a.Name == name).FirstOrDefault();
            var model = mapper.Map<AssetTypeViewModel>(assetType);
            return View(model);
        }

        [HttpPost]
        public IActionResult RemoveProperty(string assetTypeId, string property)
        {
            assetTypeCommands.RemoveProperty(assetTypeId, property);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddProperty(string assetTypeId, string property)
        {
            assetTypeCommands.AddProperty(assetTypeId, property);
            return Ok();
        }

        [HttpPost]
        public IActionResult RenameProperty(string assetTypeId, string property, string newName)
        {
            assetTypeCommands.RenameProperty(assetTypeId, property, newName);
            return Ok();
        }

        [HttpPost]
        public IActionResult EditName(string assetTypeId, string name)
        {
            assetTypeCommands.UpdateName(assetTypeId, name);
            return Ok();
        }
    }
}