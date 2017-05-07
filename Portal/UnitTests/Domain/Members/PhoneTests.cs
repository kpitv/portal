using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Portal.Domain.Members;
using Portal.Domain.Shared;
using Xunit;

namespace Portal.Tests.UnitTests.Domain.Members
{
    public class PhoneTests
    {
        private const string validNumber = "+380965687423";

        [Theory]
        [InlineData("380965687423")]
        [InlineData("+3809656874232")]
        [InlineData("+38096568742")]
        [InlineData("+38096 5687423")]
        public void Ctor_NumbersShouldThrowException(string number)
        {
            var action = new Action(() => new Phone(number));

            action.ShouldThrow<DomainException<string>>();
        }

        [Fact]
        public void Ctor_NumbersShouldBeEqual()
        {
            var phone = new Phone(validNumber);

            phone.Number.Should().Be(validNumber);
        }

        [Theory]
        [InlineData("+380935687423")]
        [InlineData("+380325687422")]
        [InlineData("+380505687423")]
        public void Operator_ShouldNotBeKyivstar(string number)
        {
            var phone = new Phone(number);

            phone.Operator.Should().NotBe(Operator.Kyivstar);
        }

        [Fact]
        public void Operator_ShouldBeKyivstar()
        {
            var phone = new Phone(validNumber);

            phone.Operator.Should().Be(Operator.Kyivstar);
        }

        [Fact]
        public void NumberWithBracesAndDashes_ShouldBeEqual()
        {
            var phone = new Phone(validNumber);
            string bracesAndDashes = "+380 (96) 568-74-23";

            phone.NumberWithBracesAndDashes.Should().Be(bracesAndDashes);
        }

        [Fact]
        public void NumberWithSpaces_ShouldBeEqual()
        {
            var phone = new Phone(validNumber);
            string spaces = "+380 96 568 7423";

            phone.NumberWithSpaces.Should().Be(spaces);
        }
    }
}
