using System.Collections.Generic;

namespace Portal.Persistance.Assets.Entities
{
    public class AssetTypeEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<AssetTypePropertyEntity> Properties { get; set; }
    }
}
