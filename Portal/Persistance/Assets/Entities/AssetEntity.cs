using System.Collections.Generic;

namespace Portal.Persistance.Assets.Entities
{
    public class AssetEntity
    {
        public string Id { get; set; }
        public string AssetTypeEntityId { get; set; }
        public AssetTypeEntity AssetType { get; set; }
        public List<AssetPropertyValueEntity> Values { get; set; }
    }
}
