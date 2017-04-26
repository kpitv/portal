using Portal.Application.Assets.Commands.Models;

namespace Portal.Application.Assets.Commands
{
    public interface IAssetTypeCommands
    {
        void AddAsset(string assetTypeId, AssetModel model);
        void Create(CreateAssetTypeModel model);
        void RemoveAsset(string assetTypeId, AssetModel model);
        void UpdateName(string assetTypeId, string name);
        void RemoveProperty(string assetTypeId, string property);
        void RenameProperty(string assetTypeId, string property, string newName);
        void AddProperty(string assetTypeId, string property);
        void MoveProperty(string assetTypeId, string property, int index);
        void UpdateAsset(string assetTypeId, AssetModel model);
    }
}