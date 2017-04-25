using Portal.Application.Shared;
using Portal.Persistance.Members.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using Portal.Domain.Assets;
using Portal.Domain.Members;
using Portal.Persistance.Assets.Entities;

namespace Portal.Persistance.Shared
{
    public static class EntityMapper
    {
        #region Member
        public static Member ToMember(this MemberEntity memberEntity) =>
           new Member(
                id: Guid.Parse(memberEntity.Id),
                userId: memberEntity.UserId,
                name: new MemberName(
                    firstName: new LangSet(memberEntity.FirstNameInEnglish, memberEntity.FirstNameInRussian, memberEntity.FirstNameInUkrainian),
                    secondName: new LangSet(memberEntity.SecondNameInEnglish, memberEntity.SecondNameInRussian, memberEntity.SecondNameInUkrainian),
                    lastName: new LangSet(memberEntity.LastNameInEnglish, memberEntity.LastNameInRussian, memberEntity.LastNameInUkrainian)),
                email: memberEntity.Email,
                phones: memberEntity.Phones.ToMappedCollection(ToPhone).ToList(),
                roles: memberEntity.Roles.ToMappedCollection(ToRole).ToList(),
                contactLinks: memberEntity.ContactLinks.ToMappedCollection(ToContactLink)
                    .Zip(memberEntity.ContactLinks.Select(c => c.Link), (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v),
                about: memberEntity.About
                );

        public static Phone ToPhone(this PhoneEntity phoneEntity) =>
            new Phone(phoneEntity.Number);

        public static Role ToRole(this RoleEntity roleEntity) =>
            Enum.TryParse<Role>(roleEntity.Name, out var result) ? result : Role.None;

        public static ContactLink ToContactLink(this ContactLinkEntity contactLinkEntity) =>
            (ContactLink)Enum.Parse(typeof(ContactLink), contactLinkEntity.Contact);

        public static MemberEntity ToMemberEntity(this Member member) =>
            new MemberEntity
            {
                Id = member.Id.ToString(),
                UserId = member.UserId,
                FirstNameInEnglish = member.Name.FirstName.InEnglish,
                FirstNameInRussian = member.Name.FirstName.InRussian,
                FirstNameInUkrainian = member.Name.FirstName.InUkrainian,
                SecondNameInEnglish = member.Name.SecondName.InEnglish,
                SecondNameInRussian = member.Name.SecondName.InRussian,
                SecondNameInUkrainian = member.Name.SecondName.InUkrainian,
                LastNameInEnglish = member.Name.LastName.InEnglish,
                LastNameInRussian = member.Name.LastName.InRussian,
                LastNameInUkrainian = member.Name.LastName.InUkrainian,
                Email = member.Email,
                Phones = member.Phones.ToMappedCollection(a => a.ToPhoneEntity(member.Id.ToString())).ToList(),
                Roles = member.Roles.ToMappedCollection(a => a.ToRoleEntity(member.Id.ToString())).ToList(),
                ContactLinks = member.ContactLinks?.ToContactLinkEntities(member.Id.ToString()),
                About = member.About
            };

        public static PhoneEntity ToPhoneEntity(this Phone phone, string memberId) =>
            new PhoneEntity { Number = phone.Number, MemberId = memberId };

        public static RoleEntity ToRoleEntity(this Role role, string memberId) =>
            new RoleEntity { Name = role.ToString(), MemberId = memberId };

        public static List<ContactLinkEntity> ToContactLinkEntities(
            this Dictionary<ContactLink, string> contactLinks, string memberId) =>
            contactLinks.Select(item => new ContactLinkEntity
            {
                Contact = item.Key.ToString(),
                Link = item.Value,
                MemberId = memberId
            })
            .ToList();
        #endregion

        #region Asset
        public static AssetType ToAssetType(this AssetTypeEntity assetTypeEntity) =>
            new AssetType(
                id: Guid.Parse(assetTypeEntity.Id),
                name: assetTypeEntity.Name,
                properties: assetTypeEntity.Properties.OrderBy(p => p.Index).Select(p => p.Name).ToList()
                );

        public static Asset ToAsset(this AssetEntity assetEntity) =>
            new Asset(
                id: Guid.Parse(assetEntity.Id),
                values: assetEntity.Values.OrderBy(v => v.Index).Select(v => v.Value).ToList()
                );

        public static AssetTypeEntity ToAssetTypeEntity(this AssetType assetType)
        {
            var assetTypeEntity = new AssetTypeEntity
            {
                Id = assetType.Id.ToString(),
                Name = assetType.Name
            };

            for (int i = 0; i < assetType.Properties.Count; i++)
                assetTypeEntity.Properties.Add(assetType.Properties[i]
                    .ToAssetTypePropertyEntity(assetType.Id.ToString(), i));
            
            return assetTypeEntity;
        }

        public static AssetEntity ToAssetEntity(this Asset asset, AssetType assetType)
        {
            return new AssetEntity
            {
                Id = asset.Id.ToString(),
                AssetTypeEntityId = assetType.Id.ToString(),
                Values = asset.Values.Select((t, i) => new AssetPropertyValueEntity
                    {
                        AssetEntityId = asset.Id.ToString(),
                        Value = t,
                        PropertyName = assetType.Properties[i],
                        PropertyAssetTypeEntityId = assetType.Id.ToString(),
                        Index = i
                    })
                    .ToList()
            };
        }

        private static AssetTypePropertyEntity ToAssetTypePropertyEntity(this string assetTypeProperty,
            string assetTypeId, int index) =>
            new AssetTypePropertyEntity
            {
                Name = assetTypeProperty,
                AssetTypeEntityId = assetTypeId,
                Index = index
            };
        #endregion
    }
}
