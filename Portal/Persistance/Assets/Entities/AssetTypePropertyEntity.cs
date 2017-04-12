namespace Portal.Persistance.Assets.Entities
{
    public class AssetTypePropertyEntity
    {
        public string Name { get; set; }
        public string AssetTypeEntityId { get; set; }
        public AssetTypeEntity AssetType { get; set; }
    }
}
