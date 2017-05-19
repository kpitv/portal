using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Presentation.MVC.Assets.ViewModels
{
    public class AssetTypeViewModel
    {
        public ILookup<string, string> Errors { get; set; } =
            new Dictionary<string, string>().ToLookup(e => e.Key, e => e.Value);

        [HiddenInput]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public List<string> Properties { get; set; }
    }
}
