using System;
using System.Collections.Generic;

namespace Portal.Application.Assets.Commands.Models
{
    public class UpdateAssetTypeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Properties { get; set; }
    }
}