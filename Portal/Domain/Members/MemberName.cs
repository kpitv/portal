using System;
using System.Text.RegularExpressions;
using Portal.Domain.Shared;

namespace Portal.Domain.Members
{
    public class MemberName : ValueObject<MemberName>
    {
        #region Properties
        public LangSet FirstName { get; }
        public LangSet SecondName { get; }
        public LangSet LastName { get; }
        public LangSet LastFirstSecondName => new LangSet(
            $"{LastName.InEnglish} {FirstName.InEnglish}",
            $"{LastName.InRussian} {FirstName.InRussian} {SecondName.InRussian}",
            $"{LastName.InUkrainian} {FirstName.InUkrainian} {SecondName.InUkrainian}");
        public LangSet FirstLastName => new LangSet(
            $"{FirstName.InEnglish} {LastName.InEnglish}",
            $"{FirstName.InRussian} {LastName.InRussian}",
            $"{FirstName.InUkrainian} {LastName.InUkrainian}");
        public LangSet LastNameWithInitials
        {
            get
            {
                string secondName = SecondName.InEnglish == "" ? "" : $" {SecondName.InEnglish}.";
                return new LangSet($"{LastName.InEnglish} {FirstName.InEnglish[0]}.{secondName}",
                    $"{LastName.InRussian} {FirstName.InRussian[0]}. {SecondName.InRussian[0]}.",
                    $"{LastName.InUkrainian} {FirstName.InUkrainian[0]}. {SecondName.InUkrainian[0]}.");
            }
        }
        #endregion

        public MemberName(LangSet firstName, LangSet secondName, LangSet lastName)
        {
            if (Validate(firstName) && Validate(secondName) && Validate(lastName))
            {
                FirstName = firstName;
                SecondName = secondName;
                LastName = lastName;
            }
            else throw new ArgumentException("Invalid input");
        }

        #region Methods
        public static bool Validate(LangSet name) =>
           Regex.IsMatch(name.InEnglish, @"^[a-zA-Z\-']*$") &&
           Regex.IsMatch(name.InRussian, @"^[а-яА-яЁё\-']*$") &&
           Regex.IsMatch(name.InUkrainian, @"^[А-Ща-щЬЮЯьюяҐІЇЄґіїє\-']*$");

        public MemberName Update(LangSet firstName = null,
            LangSet secondName = null, LangSet lastName = null) =>
            new MemberName(firstName ?? FirstName, secondName ?? SecondName, lastName ?? LastName);

        protected override bool EqualsCore(MemberName other) =>
            FirstName == other.FirstName &&
            SecondName == other.SecondName &&
            LastName == other.LastName;

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = 1;
                hashCode = (hashCode * 397) ^ (FirstName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (SecondName?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (LastName?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
        #endregion
    }
}
