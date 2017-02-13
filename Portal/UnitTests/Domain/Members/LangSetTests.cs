using System;
using FluentAssertions;
using Portal.Domain.Members;
using Xunit;

namespace Portal.Tests.UnitTests.Domain.Members
{
    public class LangSetTests
    {
        [Theory]
        [InlineData("Волянскёй", "Волянский", "Волянській")]
        [InlineData("Volyansky", "Vолянский", "Волянській")]
        [InlineData("Volyansky", "Волянский", "Vолянській")]
        public void Ctor_IfNotCorrespondToLangShouldThrowException(string eng, string rus, string ukr)
        {
            Action action = () => new LangSet(eng, rus, ukr);

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Update_ShouldReturnEqualTo()
        {
            var obj = new LangSet("Hello", "Привет", "Привіт");
            var objUpdated = new LangSet("Volyansky", "Волянский", "Привіт");

            obj = obj.Update(inEnglish: "Volyansky", inRussian: "Волянский");

            obj.Should().Be(objUpdated);
        }

        [Theory]
        [InlineData("Волянскёй")]
        [InlineData("Vолянский")]
        [InlineData("Кiричок")]
        public void CanBeUkrainian_IfNotUkrainianShouldReturnFalse(string text)
        {
            bool result = LangSet.CanBeUkrainian(text);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("Волянськії")]
        [InlineData("Егор")]
        public void CanBeUkrainian_IfUkrainianShouldReturnFalse(string text)
        {
            bool result = LangSet.CanBeUkrainian(text);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("Волянськії")]
        [InlineData("Vолянский")]
        [InlineData("Кiричок")]
        public void CanBeRussian_IfNotRussianShouldReturnFalse(string text)
        {
            bool result = LangSet.CanBeRussian(text);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("Волянськё")]
        [InlineData("Егор")]
        public void CanBeRussian_IfRussianShouldReturnFalse(string text)
        {
            bool result = LangSet.CanBeRussian(text);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("Vоlyansky")]
        [InlineData("Shoolzъ")]
        [InlineData("Їdalnya")]
        public void CanBeEnglish_IfNotEnglishShouldReturnFalse(string text)
        {
            bool result = LangSet.CanBeEnglish(text);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("Volyansky")]
        [InlineData("XPAM XAKEPCTBA!!!")]
        public void CanBeEnglish_IfEnglishShouldReturnFalse(string text)
        {
            bool result = LangSet.CanBeEnglish(text);

            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldNotBeEqual()
        {
            var obj1 = new LangSet("Hello!", "Привет!", "Вітаю!");
            var obj2 = new LangSet("Hello!", "Привет! ", "Вітаю!");

            obj1.Should().NotBe(obj2);
        }

        [Fact]
        public void Equals_ShouldBeEqual()
        {
            object obj1 = new LangSet("Hello!", "Привет!", "Вітаю!");
            LangSet obj2 = new LangSet("Hello!", "Привет!", "Вітаю!");

            obj1.Should().Be(obj2);
        }

        [Fact]
        public void GetHashCode_ShouldNotBeEqual()
        {
            var obj1 = new LangSet("Hello!", "Привет!", "Привет");
            var obj2 = new LangSet("Hello!", "Привет", "Привет!");

            obj1.GetHashCode().Should().NotBe(obj2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeEqual()
        {
            var obj1 = new LangSet("Hello!", "Привет!", "Вітаю!");
            var obj2 = new LangSet("Hello!", "Привет!", "Вітаю!");

            obj1.GetHashCode().Should().Be(obj2.GetHashCode());
        }
    }
}
