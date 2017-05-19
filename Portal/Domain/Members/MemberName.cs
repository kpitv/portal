using System;
using System.Text.RegularExpressions;
using Portal.Domain.Shared;
using static Portal.Domain.Shared.ValidationError;

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
            if (firstName is null || secondName is null || lastName is null)
                throw new ArgumentNullException();

            if (Validate(firstName, nameof(FirstName)) &&
                Validate(secondName, nameof(SecondName))
                && Validate(lastName, nameof(LastName)))
            {
                FirstName = firstName;
                LastName = lastName;
                SecondName = secondName;
            }
            else
                throw new ArgumentException();
        }

        #region Methods

        public bool Validate(LangSet name, string nameType)
        {
            var state = true;
            if (!Regex.IsMatch(name.InEnglish, @"^[a-zA-Z\-']*$"))
            {
                switch (nameType)
                {
                    case nameof(FirstName):
                        RaiseError(this, new ValidationEventArgs(InvalidFirstNameInEnglish, invalidValue: name.InEnglish));
                        break;
                    case nameof(SecondName):
                        RaiseError(this, new ValidationEventArgs(InvalidSecondNameInEnglish, invalidValue: name.InEnglish));
                        break;
                    case nameof(LastName):
                        RaiseError(this, new ValidationEventArgs(InvalidLastNameInEnglish, invalidValue: name.InEnglish));
                        break;
                }
                state = false;
            }
            if (!Regex.IsMatch(name.InRussian, @"^[а-яА-яЁё\-']*$"))
            {
                switch (nameType)
                {
                    case nameof(FirstName):
                        RaiseError(this,
                            new ValidationEventArgs(InvalidFirstNameInRussian, invalidValue: name.InRussian));
                        break;
                    case nameof(SecondName):
                        RaiseError(this,
                            new ValidationEventArgs(InvalidSecondNameInRussian, invalidValue: name.InRussian));
                        break;
                    case nameof(LastName):
                        RaiseError(this,
                            new ValidationEventArgs(InvalidLastNameInRussian, invalidValue: name.InRussian));
                        break;
                }
                state = false;
            }
            if (!Regex.IsMatch(name.InUkrainian, @"^[А-Ща-щЬЮЯьюяҐІЇЄґіїє\-']*$"))
            {
                switch (nameType)
                {
                    case nameof(FirstName):
                        RaiseError(this,
                            new ValidationEventArgs(InvalidFirstNameInUkrainian, invalidValue: name.InUkrainian));
                        break;
                    case nameof(SecondName):
                        RaiseError(this,
                            new ValidationEventArgs(InvalidSecondNameInUkrainian, invalidValue: name.InUkrainian));
                        break;
                    case nameof(LastName):
                        RaiseError(this,
                            new ValidationEventArgs(InvalidLastNameInUkrainian, invalidValue: name.InUkrainian));
                        break;
                }
                state = false;
            }
            return state;
        }

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
