using System;
using System.Collections.Generic;
using FluentAssertions;
using Portal.Domain.Members;
using Portal.Domain.Members.Exceptions.Member;
using Xunit;

namespace Portal.Tests.UnitTests.Domain.Members
{
    public class MemberTests
    {
        private readonly MemberName memberName = new MemberName(
              new LangSet("Larochka", "Ларочка", "Ларочка"),
              new LangSet("", "Ивановна", "Іванівна"),
              new LangSet("Coobley", "Кублий", "Кублій")
              );

        [Fact]
        public void Ctor_IfPhonesIsNullShouldThrowException()
        {
            Action action = () => new Member("1", memberName, "fuzz@bizz.com", phones: null,
                roles: new List<Role>() { Role.Coordinator, Role.Photographer });

            action.ShouldThrow<InvalidPhoneListException>();
        }

        [Theory]
        [InlineData("qwe@qe@com")]
        [InlineData("%#@fa.com")]
        [InlineData("fa.@s.com")]
        [InlineData(".st@ds.com")]
        [InlineData("st@ds")]
        public void ValidateEmail_ShouldReturnFalse(string email)
        {
            bool result = Member.ValidateEmail(email);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("fuzz@bizz.com")]
        [InlineData("fu.zz@bizz.com")]
        [InlineData("380@025.com")]
        public void ValidateEmail_ShouldReturnTrue(string email)
        {
            bool result = Member.ValidateEmail(email);

            result.Should().BeTrue();
        }


        [Theory]
        [InlineData("")]
        public void ValidateAbout_ShouldReturnFalse(string about)
        {
            bool result = Member.ValidateAbout(about);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateAbout_IfStringLengthIsGreaterThan500ShouldReturnFalse()
        {
            bool result = Member.ValidateAbout(new string('c', 501));

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateAbout_ShouldReturnTrue()
        {
            bool result = Member.ValidateAbout("Stasik");

            result.Should().BeTrue();
        }

        private static IEnumerable<object[]> PhoneListFalseData()
        {
            yield return new object[] { null };
            yield return new object[] { new List<Phone>() };
            yield return new object[] { new List<Phone> { new Phone("+380930881522"), null } };
            yield return new object[] { new List<Phone> { new Phone("+380935555555"),
                new Phone("+380930881522"), new Phone("+380930881522") } };
        }
        [Theory, MemberData(nameof(PhoneListFalseData))]
        public void ValidatePhoneList_ShouldReturnFalse(List<Phone> phones)
        {
            bool result = Member.ValidatePhoneList(phones);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidatePhoneList_ShouldReturnTrue()
        {
            var phones = new List<Phone>{ new Phone("+380935555555"),
                new Phone("+380930881422"), new Phone("+380930881522") };

            bool result = Member.ValidatePhoneList(phones);

            result.Should().BeTrue();
        }

        private static IEnumerable<object[]> RoleListFalseData()
        {
            yield return new object[] { null };
            yield return new object[] { new List<Role>() };
            yield return new object[] { new List<Role> { Role.Cameraman,
                Role.Cameraman, Role.Coordinator } };
        }
        [Theory, MemberData(nameof(RoleListFalseData))]
        public void ValidateRoleList_ShouldReturnFalse(List<Role> roles)
        {
            bool result = Member.ValidateRoleList(roles);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateRoleList_ShouldReturnTrue()
        {
            var roles = new List<Role> { Role.Cameraman, Role.Copyrighter, Role.Coordinator };

            bool result = Member.ValidateRoleList(roles);

            result.Should().BeTrue();
        }

        private static IEnumerable<object[]> ContactLinksFalseData()
        {
            yield return new object[] { null };
            yield return new object[] { new Dictionary<ContactLink, string>() };
            yield return new object[] { new Dictionary<ContactLink, string>
            {
                { ContactLink.Facebook, "ss" }, {ContactLink.Instagram, "sy" }, {ContactLink.PersonalSite, null}
            } };
            yield return new object[] { new Dictionary<ContactLink, string>
            {
                { ContactLink.Facebook, "ss" }, {ContactLink.Instagram, "ss" }, {ContactLink.PersonalSite, "sz"}
            } };
        }
        [Theory, MemberData(nameof(ContactLinksFalseData))]
        public void ValidateContactLinks_ShouldReturnFalse(Dictionary<ContactLink, string> contactLinks)
        {
            bool result = Member.ValidateContactLinks(contactLinks);

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidateContactLinks_ShouldReturnTrue()
        {
            var contactLinks = new Dictionary<ContactLink, string>
            {
                { ContactLink.Facebook, "sx" },
                { ContactLink.Instagram, "sy" },
                { ContactLink.PersonalSite, "sz" }
            };

            bool result = Member.ValidateContactLinks(contactLinks);

            result.Should().BeTrue();
        }
    }
}
