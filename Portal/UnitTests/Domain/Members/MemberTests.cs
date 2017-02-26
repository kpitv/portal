using System;
using System.Collections.Generic;
using FluentAssertions;
using Portal.Domain.Members;
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

            action.ShouldThrow<ArgumentNullException>();
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
    }
}
