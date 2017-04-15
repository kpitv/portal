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

            var member = new Member(
               id: id,
               userId: "ss",
               name: new MemberName(
                   new LangSet("Larochka", "Ларочка", "Ларочка"),
                   new LangSet("", "Ивановна", "Іванівна"),
                   new LangSet("Coobley", "Кублий", "Кублій")),
               email: "ss@ss.ss",
               phones: new List<Phone> { new Phone("+380931234567"), new Phone("+380939379992") },
               roles: new List<Role> { Role.Copyrighter, Role.FilmEditor },
               contactLinks: new Dictionary<ContactLink, string> { { ContactLink.Facebook, "ss" }, { ContactLink.Instagram, "ss" } },
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
                ContactLinks = new List<ContactLinkEntity> { new ContactLinkEntity { Contact = "Facebook", Link = "ss" }, new ContactLinkEntity { Contact = "Instagram", Link = "ss" } },
                About = "ss"
            };

            var newMember = memberEntity.ToMember();

            member.ShouldBeEquivalentTo(newMember);
        }

        [Fact]
        public void ToMemberEntity_ShouldBeEqual()
        {
            var id = Guid.NewGuid();

            var member = new Member(
               id: id,
               userId: "ss",
               name: new MemberName(
                   new LangSet("Larochka", "Ларочка", "Ларочка"),
                   new LangSet("", "Ивановна", "Іванівна"),
                   new LangSet("Coobley", "Кублий", "Кублій")),
               email: "ss@ss.ss",
               phones: new List<Phone> { new Phone("+380931234567"), new Phone("+380939379992") },
               roles: new List<Role> { Role.Copyrighter, Role.FilmEditor },
               contactLinks: new Dictionary<ContactLink, string> { { ContactLink.Facebook, "ss" }, { ContactLink.Instagram, "ss" } },
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
                ContactLinks = new List<ContactLinkEntity> { new ContactLinkEntity { MemberId = id.ToString(), Contact = "Facebook", Link = "ss" }, new ContactLinkEntity { MemberId = id.ToString(), Contact = "Instagram", Link = "ss" } },
                About = "ss"
            };

            var newMemberEntity = member.ToMemberEntity();

            memberEntity.ShouldBeEquivalentTo(newMemberEntity);
        }

        //[Fact]
        //public void ToAssetEntity_ShouldBeEqual()
        //{
        //    var id = Guid.NewGuid();
        //    var assetTypeEntity = new AssetTypeEntity()
        //    {
        //        Id = Guid.NewGuid().ToString()

        //    };

        //    var asset = new Asset(
        //       id: id,
        //       values: new List<string>() { "Canon", "priceless", "60D" }
        //       );

        //    var assetEntity = new AssetEntity()
        //    {
        //        Id = id.ToString(),
        //        AssetType = assetTypeEntity,
        //        AssetTypeEntityId = assetTypeEntity.Id,
        //        Values = new List<AssetPropertyValueEntity>()
        //    };

        //    var newAssetEntity = asset.ToAssetEntity(assetType);

        //    assetEntity.ShouldBeEquivalentTo(newAssetEntity);
        //}
        [Fact]
        public void ToAssetType_ShouldBeEqual()
        {
            string id = Guid.NewGuid().ToString();

            var assetType = new AssetType("name", new List<string> { "name", "model", "price" }, Guid.Parse(id));

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

            var assetType = new AssetType("name", new List<string> { "name", "model", "price" }, Guid.Parse(id));

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

            var newAssetTypeEntity = assetType.ToAssetTypeEntity();

            assetTypeEntity.ShouldBeEquivalentTo(newAssetTypeEntity);
        }
    }
}
