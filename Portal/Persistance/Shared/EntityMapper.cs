using Portal.Application.Shared;
using Portal.Persistance.Members.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using Portal.Domain.Members;

namespace Portal.Persistance.Shared
{
    public static class EntityMapper
    {
        public static Member ToMember(this MemberEntity member) =>
           new Member(
                id: Guid.Parse(member.Id),
                userId: member.UserId,
                name: new MemberName(
                    firstName: new LangSet(member.FirstNameInEnglish, member.FirstNameInRussian, member.FirstNameInUkrainian),
                    secondName: new LangSet(member.SecondNameInEnglish, member.SecondNameInRussian, member.SecondNameInUkrainian),
                    lastName: new LangSet(member.LastNameInEnglish, member.LastNameInRussian, member.LastNameInUkrainian)),
                email: member.Email,
                phones: member.Phones.ToMappedCollection(ToPhone).ToList(),
                roles: member.Roles.ToMappedCollection(ToRole).ToList(),
                contactLinks: member.ContactLinks.ToMappedCollection(ToContactLink)
                    .Zip(member.ContactLinks.Select(c => c.Link), (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v),
                about: member.About
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
                ContactLinks = member.ContactLinks.ToContactLinkEntities(member.Id.ToString()),
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
    }
}
