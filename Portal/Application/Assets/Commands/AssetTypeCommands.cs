using System;
using System.Linq;
using Portal.Application.Assets.Commands.Models;
using Portal.Application.Interfaces;
using Portal.Domain.Assets;

namespace Portal.Application.Assets.Commands
{
    public class AssetTypeCommands : IAssetTypeCommands
    {
        private readonly IRepository<AssetType> repository;

        public AssetTypeCommands(IRepository<AssetType> repository)
        {
            this.repository = repository;
        }

        public void AddAsset(string assetTypeId, AssetModel model)
        {
            if (!Guid.TryParse(assetTypeId, out Guid assetTypeGuid))
                throw new ArgumentException("Invalid Id");
            var assetType = repository.Get(assetTypeGuid);
            assetType.AddAsset(new Asset(model.Values));
            repository.Update(assetType);
            repository.Save();
        }

        public void Create(CreateAssetTypeModel model)
        {
            repository.Create(new AssetType(
                model.Name,
                model.Properties));
            repository.Save();
        }

        public void RemoveAsset(string assetTypeId, AssetModel model)
        {
            if (!Guid.TryParse(assetTypeId, out Guid assetTypeGuid) ||
                !Guid.TryParse(model.Id, out Guid assetGuid))
                throw new ArgumentException("Invalid Id");
            var assetType = repository.Get(assetTypeGuid);
            assetType.RemoveAsset(new Asset(
                model.Values,
                assetGuid));
            repository.Update(assetType);
            repository.Save();
        }

        public void UpdateName(string assetTypeId, string name)
        {
            if (!Guid.TryParse(assetTypeId, out Guid assetTypeGuid))
                throw new ArgumentException("Invalid Id");
            var assetType = repository.Get(assetTypeGuid);
            assetType.UpdateName(name);
            repository.Update(assetType);
            repository.Save();
        }

        public void RemoveProperty(string assetTypeId, string property)
        {
            if (!Guid.TryParse(assetTypeId, out Guid assetTypeGuid))
                throw new ArgumentException("Invalid Id");
            var assetType = repository.Get(assetTypeGuid);
            assetType.RemoveProperty(property);
            repository.Update(assetType);
            repository.Save();
        }

        public void RenameProperty(string assetTypeId, string property, string newName)
        {
            if (!Guid.TryParse(assetTypeId, out Guid assetTypeGuid))
                throw new ArgumentException("Invalid Id");
            var assetType = repository.Get(assetTypeGuid);
            assetType.RenameProperty(property, newName);
            repository.Update(assetType);
            repository.Save();
        }

        public void AddProperty(string assetTypeId, string property)
        {
            if (!Guid.TryParse(assetTypeId, out Guid assetTypeGuid))
                throw new ArgumentException("Invalid Id");
            var assetType = repository.Get(assetTypeGuid);
            assetType.AddProperty(property);
            repository.Update(assetType);
            repository.Save();
        }
        public void MoveProperty(string assetTypeId, string property, int index)
        {
            if (!Guid.TryParse(assetTypeId, out Guid assetTypeGuid))
                throw new ArgumentException("Invalid Id");
            var assetType = repository.Get(assetTypeGuid);
            assetType.MoveProperty(property, index);
            repository.Update(assetType);
            repository.Save();
        }

        public void UpdateAsset(string assetTypeId, AssetModel model)
        {
            if (!Guid.TryParse(assetTypeId, out Guid assetTypeGuid) ||
                !Guid.TryParse(model.Id, out Guid assetGuid))
                throw new ArgumentException("Invalid Id");
            var assetType = repository.Get(assetTypeGuid);
            assetType.Assets.FirstOrDefault(a => a.Id == assetGuid).Update(model.Values);
            repository.Update(assetType);
            repository.Save();
        }
    }
}