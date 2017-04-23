using System;
using System.Collections.Generic;
using Portal.Domain.Assets;

namespace Portal.Application.Assets.Queries
{
    public interface IAssetTypeQueries
    {
        AssetType GetAssetType(string assetTypeId);
        IEnumerable<AssetType> GetAssetTypes();
        IEnumerable<AssetType> FindAssetTypes(Predicate<AssetType> predicate);
    }
}