using Xunit;
using Portal.Domain.Assets;
using FluentAssertions;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Portal.Tests.UnitTests.Domain.Assets
{
    public class AssetTypeTests
    {
        private AssetType assetType = new AssetType("name", new List<string> { "name", "model", "price" });

        [Fact]
        public void AddAsset_ShouldThrowArgumentExeption()
        {
            Action action = () => assetType.AddAsset(new Asset(new List<string> { "stasik", "dimasik" }));

            action.ShouldThrow<ArgumentException>();

        }

        [Fact]
        public void AddAsset_ShouldAddAsset()
        {
            var asset = new Asset(new List<string> { "stasik", "dimasik", "chugaevsky" });
            var assets = new List<Asset> { asset };

            assetType.AddAsset(asset);

            assetType.Assets.ToList().ShouldBeEquivalentTo(assets);
        }

        [Fact]
        public void RemoveAsset_ShouldRemoveAsset()
        {
            var asset1 = new Asset(new List<string> { "stasik", "dimasik", "chugaevsky" });
            var asset2 = new Asset(new List<string> { "stasik", "chugaevsky", "dimasik" });
            var assets = new List<Asset> { asset2 };

            assetType.AddAsset(asset1);
            assetType.AddAsset(asset2);
            assetType.RemoveAsset(asset1);

            assetType.Assets.ShouldBeEquivalentTo(assets);
        }

        [Theory]
        [InlineData("aaaaaaaaaaa")]
        [InlineData("")]
        [InlineData("\t\n ")]
        [InlineData(null)]
        public void ValidateName_ShouldReturnFalse(string name)
        {
            bool result = AssetType.ValidateName(name, 10);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateName_ShouldReturnTrue()
        {
            bool result = AssetType.ValidateName("Dimasuk", 10);

            result.Should().BeTrue();
        }

        public static IEnumerable<object[]> ValidatePropertiesFalseData()
        {
            yield return new object[] { new List<string> { "", "lol" } };
            yield return new object[] { new List<string>() };
        }

        [Theory, MemberData(nameof(ValidatePropertiesFalseData))]
        public void ValidateProperties_ShouldReturnFalse(List<string> properties)
        {
            bool result = AssetType.ValidateProperties(properties);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateProperties_ShouldThrowNullReferenceException()
        {
            bool result =  AssetType.ValidateProperties(null);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateProperties_ShouldReturnTrue()
        {
            bool result = AssetType.ValidateProperties(new List<string> { "name", "model", "price" });

            result.Should().BeTrue();
        }

        [Fact]
        public void UpdateName_ShouldBeEqual()
        {
            var updatedAssetType = new AssetType("newName", new List<string> { "name", "model", "price" });

            assetType.UpdateName("newName");

            assetType.Name.Should().Be(updatedAssetType.Name);
        }

        [Theory]
        [InlineData("lol", 2)]
        [InlineData("name", 10)]
        public void MoveProperty_ShouldThrowException(string name, int index)
        {
            System.Action action = () => assetType.MoveProperty(name, index);

            action.ShouldThrow<System.Exception>();
        }

        [Fact]
        public void MoveProperty_ShouldBeEqual()
        {
            var updatedAssetType = new AssetType("name", new List<string> { "model", "name", "price" });

            assetType.MoveProperty("name", 1);

            assetType.Properties.Should().BeEquivalentTo(updatedAssetType.Properties);
        }

        [Theory]
        [InlineData("name", 2)]
        [InlineData("sss", 10)]
        public void AddProperty_ShouldThrowException(string name, int index)
        {
            System.Action action = () => assetType.AddProperty(name, index);

            action.ShouldThrow<System.Exception>();
        }

        [Fact]
        public void AddProperty_ShouldBeEqual()
        {
            var updatedAssetType = new AssetType("name", new List<string> { "name", "model", "lol", "price" });

            assetType.AddProperty("lol", 2);

            assetType.Properties.Should().BeEquivalentTo(updatedAssetType.Properties);
        }

        [Fact]
        public void RemoveProperty_ShouldThrowException()
        {
            System.Action action = () => assetType.RemoveProperty("ds");

            action.ShouldThrow<System.Exception>();
        }

        [Fact]
        public void RemoveProperty_ShouldBeEqual()
        {
            var updatedAssetType = new AssetType("name", new List<string> { "name", "model" });

            assetType.RemoveProperty("price");

            assetType.Properties.Should().BeEquivalentTo(updatedAssetType.Properties);
        }
    }
}
