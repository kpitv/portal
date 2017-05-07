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

        public IEnumerable<AssetType> Find(Predicate<AssetType> predicate)
        {
            try
            {
                return databaseService.AssetTypes.Include(a => a.Properties)
                        .ToMappedCollection(EntityMapper.ToAssetType)
                        .Where(m => predicate(m));
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Find), nameof(AssetType));
            }
        }

        public AssetType Get(Guid id)
        {
            try
            {
                return databaseService.AssetTypes.AsNoTracking()
                         .Include(a => a.Properties)
                         .AsNoTracking()
                         .Single(a => a.Id == id.ToString())
                         .ToAssetType();
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Get), nameof(AssetType));
            }
        }

        public IEnumerable<AssetType> GetAll()
        {
            try
            {
                return databaseService.AssetTypes.Include(a => a.Properties).ToMappedCollection(EntityMapper.ToAssetType);
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(GetAll), nameof(AssetType));
            }
        }

        #endregion

        #region Commands
        public void Create(AssetType aggregateRoot)
        {
            try
            {
                databaseService.AssetTypes.Add(aggregateRoot.ToAssetTypeEntity());
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Create), nameof(AssetType));
            }
        }

        public void Update(AssetType aggregateRoot)
        {
            try
            {
                databaseService.AssetTypes.Update(aggregateRoot.ToAssetTypeEntity());
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Update), nameof(AssetType));
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                databaseService.AssetTypes.Remove(databaseService.AssetTypes.Single(a => a.Id == id.ToString()));
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Delete), nameof(AssetType));
            }
        }

        public void Save()
        {
            try
            {
                databaseService.SaveChanges();
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Save), nameof(AssetType), isStorageException: true);
            }
        }

        public void DetachAllEntities()
        {
            databaseService.DetachAllEntities();
        }
        #endregion
    }
}
