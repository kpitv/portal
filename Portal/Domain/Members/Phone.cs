using System;
using System.Text.RegularExpressions;
using Portal.Domain.Members.Exceptions.Phone;
using Portal.Domain.Shared;

namespace Portal.Domain.Members
{
    public class Phone : ValueObject<Phone>
    {
        #region Properties
        private string OperatorCode { get; }
        public Operator Operator
        {
            get
            {
                switch (OperatorCode)
                {
                    case "67":
                    case "68":
                    case "96":
                    case "97":
                    case "98":
                        return Operator.Kyivstar;
                    case "63":
                    case "73":
                    case "93":
                        return Operator.Lifecell;
                    case "50":
                    case "66":
                    case "95":
                    case "99":
                        return Operator.Vodafone;
                    default:
                        return Operator.Other;
                }
            }
        }
        // +380230202020
        public string Number { get; }
        // +380 (23) 020-20-20
        public string NumberWithBracesAndDashes =>
            Number.Insert(4, " (").Insert(8, ") ").Insert(13, "-").Insert(16, "-");
        // +380 23 020 2020
        public string NumberWithSpaces =>
            Number.Insert(4, " ").Insert(7, " ").Insert(11, " ");
        #endregion

        public Phone(string number)
        {
            if (number is null) throw new NullReferenceException();

            if (Regex.IsMatch(number, @"^\+380\d{9}$"))
            {
                OperatorCode = number.Substring(4, 2);
                Number = number;
            }
            else throw new InvalidPhoneException(nameof(InvalidPhoneException));
        }

        #region Methods
        protected override bool EqualsCore(Phone other) =>
            Number == other.Number;
        protected override int GetHashCodeCore()
        {
            unchecked
            {
                return 397 ^ (Number?.GetHashCode() ?? 0);
            }
        }
        #endregion
    }
}
