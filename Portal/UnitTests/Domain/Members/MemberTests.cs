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

        public IEnumerable<object[]> InvalidCtorData()
        {
            yield return new object[] { new Member(Guid.NewGuid(), memberName, "fuzz@bizz.com", null, new List<Role>() { Role.Coordinator, Role.Photographer }), };
            yield return new object[] { new LangSet("Hello", "/Привет", "Привіт") };
        }

        [Theory]
        [MemberData(nameof(InvalidCtorData))]
        public void Ctor_ShouldThrowException()
        {

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
    }
}
