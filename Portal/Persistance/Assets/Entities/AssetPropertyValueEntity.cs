using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Portal.Persistance.Assets.Entities
{
    public class AssetPropertyValueEntity
    {
        public string Value { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAssetTypeEntityId { get; set; }
        public AssetTypePropertyEntity Property { get; set; }
        public string AssetEntityId { get; set; }
        public AssetEntity Asset { get; set; }
        public int Index { get; set; }
    }
}
