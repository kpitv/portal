using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Portal.Application.Interfaces;
using Portal.Application.Shared;
using Portal.Domain.Assets;
using Portal.Persistance.Shared;

namespace Portal.Persistance.Assets
{
    public class AssetRepository : IRepository<AssetType>
    {
        private readonly DatabaseService databaseService;

        public AssetRepository(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        #region Queries
        public IEnumerable<AssetType> Find(Predicate<AssetType> predicate) =>
            databaseService.AssetTypes.Include(a => a.Properties).ToMappedCollection(EntityMapper.ToAssetType).Where(m => predicate(m));

        public AssetType Get(Guid id) =>
            databaseService.AssetTypes.AsNoTracking().Include(a => a.Properties).AsNoTracking().Single(a => a.Id == id.ToString()).ToAssetType();

        public IEnumerable<AssetType> GetAll() =>
            databaseService.AssetTypes.Include(a => a.Properties).ToMappedCollection(EntityMapper.ToAssetType);
        #endregion

        #region Commands
        public void Create(AssetType aggregateRoot)
        {
            databaseService.AssetTypes.Add(aggregateRoot.ToAssetTypeEntity());
        }

        public void Update(AssetType aggregateRoot)
        {
            databaseService.AssetTypes.Update(aggregateRoot.ToAssetTypeEntity());
        }

        public void Delete(Guid id)
        {
            databaseService.AssetTypes.Remove(databaseService.AssetTypes.Single(a => a.Id == id.ToString()));
        }

        public void Save()
        {
            databaseService.SaveChanges();
        }

        public void DetachAllEntities()
        {
            databaseService.DetachAllEntities();
        }
        #endregion
    }
}
