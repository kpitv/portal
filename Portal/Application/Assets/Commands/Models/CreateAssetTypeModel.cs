using System.Collections.Generic;

namespace Portal.Application.Assets.Commands.Models
{
    public class CreateAssetTypeModel
    {
        public string Name { get; set; }
        public List<string> Properties { get; set; }
    }
}
