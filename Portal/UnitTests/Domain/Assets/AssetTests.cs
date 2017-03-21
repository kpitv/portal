using Xunit;
using Portal.Domain.Assets;
using FluentAssertions;
using System.Collections.Generic;

namespace Portal.Tests.UnitTests.Domain.Assets
{
    public class AssetTests
    {
        private readonly AssetType assetType;
        private readonly Asset asset;

        public AssetTests()
        {
            assetType = new AssetType("Camera", new List<string> { "name", "model", "price" });
            asset = new Asset(new List<string> { "Canon", "60D", "priceless" });
            assetType.AddAsset(asset);

        }

        [Fact]
        public void OnPropertyMoved_ShouldMovePropertyInValues()
        {
            var newAsset = assetType.Assets[0];
            var updatedAsset = new Asset(new List<string> { "Canon", "priceless", "60D" });

            assetType.MoveProperty("price", 1);

            newAsset.Values.Should().BeEquivalentTo(updatedAsset.Values);
        }


        [Fact]
        public void OnPropertyAdded_ShouldAddPropertyToValues()
        {
            var newAsset = assetType.Assets[0];
            var updatedAsset = new Asset(new List<string> { "Canon", "60D", "priceless", "" });

            assetType.AddProperty("about");

            newAsset.Values.Should().BeEquivalentTo(updatedAsset.Values);
        }

        [Fact]
        public void OnPropertyRemoved_ShouldRemovePropertyFromValues()
        {
            var newAsset = assetType.Assets[0];
            var updatedAsset = new Asset(new List<string> { "Canon", "priceless"});

            assetType.RemoveProperty("model");

            newAsset.Values.Should().BeEquivalentTo(updatedAsset.Values);
        }
    }
}
