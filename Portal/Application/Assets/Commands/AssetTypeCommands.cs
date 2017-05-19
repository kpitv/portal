using System;
using System.Linq;
using Portal.Application.Assets.Commands.Models;
using Portal.Application.Errors;
using Portal.Application.Interfaces;
using Portal.Application.Shared;
using Portal.Domain.Assets;

namespace Portal.Application.Assets.Commands
{
    public class AssetTypeCommands : IAssetTypeCommands
    {
        private readonly IRepository<AssetType> repository;
        private readonly IValidationService validation;
        private readonly ErrorService error;

        public AssetTypeCommands(IRepository<AssetType> repository, 
            IValidationService validation, ErrorService error)
        {
            this.repository = repository;
            this.validation = validation;
            this.error = error;

            AssetType.ErrorOccurred += validation.DomainErrorsHandler;
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
            try
            {
                repository.Create(new AssetType(
                        model.Name,
                        model.Properties));
                repository.Save();
            }
            catch (ArgumentNullException)
            {
                throw new ApplicationException(nameof(ArgumentNullException));
            }
            catch (ArgumentException ex)
            {
                var domainErrors = validation.Errors.ToLookup(e => e.Value, e => e.Key.ToString());
                var applicationErrors = error.Errors.ToLookup(e => e.Value, e => e.Key.ToString());

                throw new ApplicationException(domainErrors
                    .Concat(applicationErrors)
                    .SelectMany(errors => errors.Select(value => new { errors.Key, value }))
                    .ToLookup(e => e.Key, e => e.value));
            }
            catch (PersistanceException ex)
            {
                throw new ApplicationException(ex.EntityName, ApplicationExceptionType.Storage);
            }
        }

        public void RemoveAsset(string assetTypeId, AssetModel model)
        {
            if (!Guid.TryParse(assetTypeId, out Guid assetTypeGuid) ||
                !Guid.TryParse(model.Id, out Guid assetGuid))
                throw new ArgumentException("Invalid Id");
            var assetType = repository.Get(assetTypeGuid);
            //assetType.RemoveAsset(new Asset(
            //    model.Values,
            //    assetGuid));
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