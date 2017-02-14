using System.Collections.Generic;
using FluentAssertions;
using Portal.Domain.Members;
using Xunit;

namespace Portal.Tests.UnitTests.Domain.Members
{
    public class MemberNameTests
    {
        private readonly MemberName memberName = new MemberName(
              new LangSet("Larochka", "Ларочка", "Ларочка"),
              new LangSet("", "Ивановна", "Іванівна"),
              new LangSet("Coobley", "Кублий", "Кублій")
              );

        [Fact]
        public void LastNameWithInitials_ShouldBeEqual()
        {
            var expectedName = new LangSet("Coobley L.", "Кублий Л. И.", "Кублій Л. І.");

            memberName.LastNameWithInitials.Should().Be(expectedName);
        }


        [Fact]
        public void LastFirstSecondName_ShouldBeEqual()
        {
            var expectedName = new LangSet("Coobley Larochka",
                "Кублий Ларочка Ивановна", "Кублій Ларочка Іванівна");

            memberName.LastFirstSecondName.Should().Be(expectedName);
        }

        [Fact]
        public void FirstLastName_ShouldBeEqual()
        {
            var expectedName = new LangSet("Larochka Coobley",
                "Ларочка Кублий", "Ларочка Кублій");

            memberName.FirstLastName.Should().Be(expectedName);
        }

        public static IEnumerable<object[]> ValidateFalseData()
        {
            yield return new object[] { new LangSet("1ssd", "Привет", "Привіт") };
            yield return new object[] { new LangSet("Hello", "/Привет", "Привіт") };
        }
        [Theory, MemberData(nameof(ValidateFalseData))]
        public void Validate_ShouldReturnFalse(LangSet name)
        {
            var result = MemberName.Validate(name);

            result.Should().BeFalse();
        }
        public static IEnumerable<object[]> ValidateTrueData()
        {
            yield return new object[] { new LangSet("Hello", "Привет", "Привіт") };
            yield return new object[] { new LangSet("Hello", "Привет", "") };
            yield return new object[] { new LangSet("Hello", "Привет", "Привіт-привіт") };
        }
        [Theory, MemberData(nameof(ValidateTrueData))]
        public void Validate_ShouldReturnTrue(LangSet name)
        {
            var result = MemberName.Validate(name);

            result.Should().BeTrue();
        }

        [Fact]
        public void Update_ShouldNotBeEqual()
        {
            string rusSecondName = "";
            var obj = new MemberName(
                new LangSet("Hello", "Привет", "Привіт"),
                new LangSet("Hello", "Привето", "Привіто"),
                new LangSet("Helloo", "Приветоо", "Привітоо")
                );
            var objUpdated = new MemberName(
              new LangSet("Hello", "Привет", "Привіт"),
              new LangSet("Hello", "Привет", "Привіто"),
              new LangSet("Helloo", "Приветоо", "Привітоо")
              );

            obj = obj.Update(secondName: obj.SecondName.Update(inRussian: rusSecondName));

            obj.Should().NotBe(objUpdated);
        }

        [Fact]
        public void Update_ShouldBeEqual()
        {
            string rusSecondName = "Бычевич";
            var obj = new MemberName(
                new LangSet("Hello", "Привет", "Привіт"),
                new LangSet("Hello", "Привето", "Привіто"),
                new LangSet("Helloo", "Приветоо", "Привітоо")
                );
            var objUpdated = new MemberName(
              new LangSet("Hello", "Привет", "Привіт"),
              new LangSet("Hello", rusSecondName, "Привіто"),
              new LangSet("Helloo", "Приветоо", "Привітоо")
              );

            obj = obj.Update(secondName: obj.SecondName.Update(inRussian: rusSecondName));

            obj.Should().Be(objUpdated);
        }
    }
}
