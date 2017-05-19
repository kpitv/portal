using System;
using System.Linq;
using Portal.Domain.Shared;
using static Portal.Domain.Shared.ValidationError;

namespace Portal.Domain.Members
{
    public class LangSet : ValueObject<LangSet>
    {
        #region Properties
        public string InEnglish { get; }
        public string InRussian { get; }
        public string InUkrainian { get; }
        #endregion

        public LangSet(string inEnglish, string inRussian, string inUkrainian)
        {
            var state = true;

            if (CanBeEnglish(inEnglish))
                InEnglish = inEnglish;
            else
            {
                RaiseError(this, new ValidationEventArgs(TextNotEnglish, invalidValue: inEnglish));
                state = false;
            }

            if (CanBeRussian(inRussian))
                InRussian = inRussian;
            else
            {
                RaiseError(this, new ValidationEventArgs(TextNotRussian, invalidValue: inRussian));
                state = false;
            }

            if (CanBeUkrainian(inUkrainian))
                InUkrainian = inUkrainian;
            else
            {
                RaiseError(this, new ValidationEventArgs(TextNotUkrainian, invalidValue: inUkrainian));
                state = false;
            }

            if (!state)
                throw new ArgumentException();
        }

        #region Methods
        public LangSet Update(string inEnglish = null,
            string inRussian = null, string inUkrainian = null) =>
            new LangSet(inEnglish ?? InEnglish, inRussian ?? InRussian, inUkrainian ?? InUkrainian);

        public static bool CanBeEnglish(string text) =>
            !text.ToUpper().Any(c => "АБВГҐДЕЄЁЖЗИІЇКЛМНОПРСТУФХЦЧШЩЇЪЫЬЭЮЯ".Contains(c));

        public static bool CanBeUkrainian(string text) =>
            !text.ToUpper().Any(c => "ABCDEFGHIJKLMNOPQRSTUVWXYZЁЪЫЭ".Contains(c));

        public static bool CanBeRussian(string text) =>
            !text.ToUpper().Any(c => "ABCDEFGHIJKLMNOPQRSTUVWXYZҐЄІЇ".Contains(c));

        protected override bool EqualsCore(LangSet other) =>
            InEnglish == other.InEnglish &&
            InRussian == other.InRussian &&
            InUkrainian == other.InUkrainian;

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = 1;
                hashCode = (hashCode * 397) ^ (InEnglish?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (InRussian?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (InUkrainian?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
        #endregion
    }
}