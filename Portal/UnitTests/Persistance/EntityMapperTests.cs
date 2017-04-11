using Xunit;
using Portal.Persistance.Members.Entities;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Portal.Persistance.Shared;
using Portal.Domain.Members;

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
                Phones = new List<PhoneEntity> { new PhoneEntity { Number = "+380931234567" }, new PhoneEntity { Number = "+380939379992" } },
                Roles = new List<RoleEntity> { new RoleEntity { Name = "Copyrighter" }, new RoleEntity { Name = "FilmEditor" } },
                ContactLinks = new List<ContactLinkEntity> { new ContactLinkEntity { Contact = "Facebook", Link = "ss" }, new ContactLinkEntity { Contact = "Instagram", Link = "ss" } },
                About = "ss"
            };

            var newMemberEntity = member.ToMemberEntity();

            newMemberEntity.ShouldBeEquivalentTo(newMemberEntity);
        }
    }
}
