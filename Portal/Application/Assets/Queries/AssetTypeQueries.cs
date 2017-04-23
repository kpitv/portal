using System;
using System.Collections.Generic;
using System.Linq;
using Portal.Application.Interfaces;
using Portal.Domain.Assets;

namespace Portal.Application.Assets.Queries
{
    public class AssetTypeQueries : IAssetTypeQueries
    {
        private readonly IRepository<AssetType> repository;

        public AssetTypeQueries(IRepository<AssetType> repository)
        {
            this.repository = repository;
        }

        public AssetType GetAssetType(string assetTypeId)
        {
            if (!Guid.TryParse(assetTypeId, out Guid assetTypeGuid))
                throw new ArgumentException("Invalid Id");
            return repository.Get(assetTypeGuid);
        }

        public IEnumerable<AssetType> GetAssetTypes() => 
            repository.GetAll();

        public IEnumerable<AssetType> FindAssetTypes(Predicate<AssetType> predicate) =>
            repository.Find(predicate);
    }
}