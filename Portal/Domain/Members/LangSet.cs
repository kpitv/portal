using System;
using System.Linq;
using Portal.Domain.Shared;

namespace Portal.Domain.Members
{
    public class LangSet : ValueObject<LangSet>
    {
        #region Properties
        public string InEnglish { get; private set; }
        public string InRussian { get; private set; }
        public string InUkrainian { get; private set; }
        #endregion

        public LangSet(string inEnglish, string inRussian, string inUkrainian)
        {
            InEnglish = inEnglish;
            InRussian = inRussian;
            InUkrainian = inUkrainian;
        }

        #region Methods
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