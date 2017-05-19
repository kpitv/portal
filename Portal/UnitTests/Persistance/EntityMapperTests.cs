using Xunit;
using Portal.Persistance.Members.Entities;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Portal.Domain.Assets;
using Portal.Persistance.Shared;
using Portal.Domain.Members;
using Portal.Persistance.Assets.Entities;

namespace Portal.Tests.UnitTests.Persistance
{
    public class EntityMapperTests
    {
        [Fact]
        public void ToMember_ShouldBeEqual()
        {
            var id = Guid.NewGuid();

            var member = Member.CreateWithId(
               id: id.ToString(),
               userId: "ss",
               name: new MemberName(
                   new LangSet("Larochka", "Ларочка", "Ларочка"),
                   new LangSet("", "Ивановна", "Іванівна"),
                   new LangSet("Coobley", "Кублий", "Кублій")),
               email: "ss@ss.ss",
               phones: new List<Phone> { new Phone("+380931234567"), new Phone("+380939379992") },
               roles: new List<Role> { Role.Copyrighter, Role.FilmEditor },
               contactLinks: new Dictionary<ContactLink, string> { { ContactLink.Facebook, "ss" }, { ContactLink.Instagram, "sy" } },
               about: "ss"
               );

            var memberEntity = new MemberEntity
            {
                Id = id.ToString(),
                UserId = "ss",
                FirstNameInEnglish = "Larochka",
                FirstNameInRussian = "Ларочка",
                FirstNameInUkrainian = "Ларочка",
                SecondNameInEnglish = "",
                SecondNameInRussian = "Ивановна",
                SecondNameInUkrainian = "Іванівна",
                LastNameInEnglish = "Coobley",
                LastNameInRussian = "Кублий",
                LastNameInUkrainian = "Кублій",
                Email = "ss@ss.ss",
                Phones = new List<PhoneEntity> { new PhoneEntity { Number = "+380931234567" }, new PhoneEntity { Number = "+380939379992" } },
                Roles = new List<RoleEntity> { new RoleEntity { Name = "Copyrighter" }, new RoleEntity { Name = "FilmEditor" } },
                ContactLinks = new List<ContactLinkEntity> { new ContactLinkEntity { Contact = "Facebook", Link = "ss" }, new ContactLinkEntity { Contact = "Instagram", Link = "sy" } },
                About = "ss"
            };

            var newMember = memberEntity.ToMember();

            member.ShouldBeEquivalentTo(newMember);
        }

        [Fact]
        public void ToMemberEntity_ShouldBeEqual()
        {
            var id = Guid.NewGuid();

            var member = Member.CreateWithId(
               id: id.ToString(),
               userId: "ss",
               name: new MemberName(
                   new LangSet("Larochka", "Ларочка", "Ларочка"),
                   new LangSet("", "Ивановна", "Іванівна"),
                   new LangSet("Coobley", "Кублий", "Кублій")),
               email: "ss@ss.ss",
               phones: new List<Phone> { new Phone("+380931234567"), new Phone("+380939379992") },
               roles: new List<Role> { Role.Copyrighter, Role.FilmEditor },
               contactLinks: new Dictionary<ContactLink, string> { { ContactLink.Facebook, "sy" }, { ContactLink.Instagram, "ss" } },
               about: "ss"
               );

            var memberEntity = new MemberEntity
            {
                Id = id.ToString(),
                UserId = "ss",
                FirstNameInEnglish = "Larochka",
                FirstNameInRussian = "Ларочка",
                FirstNameInUkrainian = "Ларочка",
                SecondNameInEnglish = "",
                SecondNameInRussian = "Ивановна",
                SecondNameInUkrainian = "Іванівна",
                LastNameInEnglish = "Coobley",
                LastNameInRussian = "Кублий",
                LastNameInUkrainian = "Кублій",
                Email = "ss@ss.ss",
                Phones = new List<PhoneEntity> { new PhoneEntity { MemberId = id.ToString(), Number = "+380931234567" }, new PhoneEntity { MemberId = id.ToString(), Number = "+380939379992" } },
                Roles = new List<RoleEntity> { new RoleEntity { MemberId = id.ToString(), Name = "Copyrighter" }, new RoleEntity { MemberId = id.ToString(), Name = "FilmEditor" } },
                ContactLinks = new List<ContactLinkEntity> { new ContactLinkEntity { MemberId = id.ToString(), Contact = "Facebook", Link = "sy" }, new ContactLinkEntity { MemberId = id.ToString(), Contact = "Instagram", Link = "ss" } },
                About = "ss"
            };

            var newMemberEntity = member.ToMemberEntity();

            memberEntity.ShouldBeEquivalentTo(newMemberEntity);
        }

        [Fact]
        public void ToAssetEntity_ShouldBeEqual()
        {
            var id = Guid.NewGuid();
            var assetTypeEntity = new AssetTypeEntity
            {
                Id = id.ToString(),
                Name = "name",
                Properties = new List<AssetTypePropertyEntity>
                {
                    new AssetTypePropertyEntity() { Name = "name", Index = 0, AssetTypeEntityId = id.ToString() },
                    new AssetTypePropertyEntity() { Name = "model", Index = 1, AssetTypeEntityId = id.ToString() },
                    new AssetTypePropertyEntity() { Name = "price", Index = 2, AssetTypeEntityId = id.ToString() }
                }
            };
            var assetId = Guid.NewGuid();
            var assetEntity = new AssetEntity()
            {
                Id = assetId.ToString(),
                AssetTypeEntityId = assetTypeEntity.Id,
                Values = new List<AssetPropertyValueEntity>
                {
                    new AssetPropertyValueEntity
                    {
                        AssetEntityId = assetId.ToString(),
                        Index = 0,
                        PropertyAssetTypeEntityId = id.ToString(),
                        PropertyName = "name",
                        Value = "Musya"
                    },
                    new AssetPropertyValueEntity
                    {
                        AssetEntityId = assetId.ToString(),
                        Index = 1,
                        PropertyAssetTypeEntityId = id.ToString(),
                        PropertyName = "model",
                        Value = "Pusya"
                    },
                    new AssetPropertyValueEntity
                    {
                        AssetEntityId = assetId.ToString(),
                        Index = 2,
                        PropertyAssetTypeEntityId = id.ToString(),
                        PropertyName = "price",
                        Value = "Dusya"
                    }
                }
            };
            var assetType = AssetType.CreateWithId(id, "name", new List<string> { "name", "model", "price" });
            var asset = Asset.CreateWithId(
               id: assetId,
               values: new List<string>() { "Musya", "Pusya", "Dusya" }
               );

            var newAssetEntity = asset.ToAssetEntity(assetType);

            assetEntity.ShouldBeEquivalentTo(newAssetEntity);
        }

        [Fact]
        public void ToAsset_ShouldBeEqual()
        {
            var id = Guid.NewGuid();
            var assetTypeEntity = new AssetTypeEntity
            {
                Id = id.ToString(),
                Name = "name",
                Properties = new List<AssetTypePropertyEntity>
                {
                    new AssetTypePropertyEntity() { Name = "name", Index = 0, AssetTypeEntityId = id.ToString() },
                    new AssetTypePropertyEntity() { Name = "model", Index = 1, AssetTypeEntityId = id.ToString() },
                    new AssetTypePropertyEntity() { Name = "price", Index = 2, AssetTypeEntityId = id.ToString() }
                }
            };
            var assetId = Guid.NewGuid();
            var assetEntity = new AssetEntity()
            {
                Id = assetId.ToString(),
                AssetTypeEntityId = assetTypeEntity.Id,
                Values = new List<AssetPropertyValueEntity>
                {
                    new AssetPropertyValueEntity
                    {
                        AssetEntityId = assetId.ToString(),
                        Index = 0,
                        PropertyAssetTypeEntityId = id.ToString(),
                        PropertyName = "name",
                        Value = "Musya"
                    },
                    new AssetPropertyValueEntity
                    {
                        AssetEntityId = assetId.ToString(),
                        Index = 1,
                        PropertyAssetTypeEntityId = id.ToString(),
                        PropertyName = "model",
                        Value = "Pusya"
                    },
                    new AssetPropertyValueEntity
                    {
                        AssetEntityId = assetId.ToString(),
                        Index = 2,
                        PropertyAssetTypeEntityId = id.ToString(),
                        PropertyName = "price",
                        Value = "Dusya"
                    }
                }
            };
            var assetType = AssetType.CreateWithId(id, "name", new List<string> { "name", "model", "price" });
            var asset = Asset.CreateWithId(
                id: assetId,
                values: new List<string>() { "Musya", "Pusya", "Dusya" }
            );

            var newAsset = assetEntity.ToAsset();

            asset.ShouldBeEquivalentTo(newAsset);
        }


        [Fact]
        public void ToAssetType_ShouldBeEqual()
        {
            string id = Guid.NewGuid().ToString();

            var assetType = AssetType.CreateWithId(Guid.Parse(id), "name", new List<string> { "name", "model", "price" });

            var assetTypeEntity = new AssetTypeEntity()
            {
                Id = id,
                Name = "name"
            };

            var properties = new List<AssetTypePropertyEntity>()
            {
                new AssetTypePropertyEntity() { Name = "name" },
                new AssetTypePropertyEntity() { Name = "model" },
                new AssetTypePropertyEntity() { Name = "price" }
            };
            properties.ForEach(p =>
            {
                p.AssetTypeEntityId = assetTypeEntity.Id;
            });
            assetTypeEntity.Properties = properties;

            var newAssetType = assetTypeEntity.ToAssetType();

            assetType.ShouldBeEquivalentTo(newAssetType);
        }

        [Fact]
        public void ToAssetTypeEntity_ShouldBeEqual()
        {
            string id = Guid.NewGuid().ToString();

            var assetType = AssetType.CreateWithId(Guid.Parse(id), "name", new List<string> { "name", "model", "price" });

            var assetTypeEntity = new AssetTypeEntity()
            {
                Id = id,
                Name = "name",
                Properties = new List<AssetTypePropertyEntity>()
                {
                    new AssetTypePropertyEntity() { Name = "name" , Index = 0, AssetTypeEntityId = id },
                    new AssetTypePropertyEntity() { Name = "model", Index = 1, AssetTypeEntityId = id },
                    new AssetTypePropertyEntity() { Name = "price", Index = 2, AssetTypeEntityId = id }
                }
            };

            var newAssetTypeEntity = assetType.ToAssetTypeEntity();

            assetTypeEntity.ShouldBeEquivalentTo(newAssetTypeEntity);
        }
    }
}
