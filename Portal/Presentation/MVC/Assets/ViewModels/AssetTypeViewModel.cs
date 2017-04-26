using System.Collections.Generic;

namespace Portal.Presentation.MVC.Assets.ViewModels
{
    public class AssetTypeViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Properties { get; set; }
    }
}
