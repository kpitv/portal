namespace Portal.Persistance.Assets.Entities
{
    public class AssetPropertyValueEntity
    {
        public string Value { get; set; }
        public AssetTypePropertyEntity Property { get; set; }
        public string AssetEntityId { get; set; }
        public AssetEntity Asset{ get; set; }
    }
}
