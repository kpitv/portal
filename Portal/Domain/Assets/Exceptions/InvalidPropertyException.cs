using Portal.Domain.Shared;

namespace Portal.Domain.Assets.Exceptions
{
    public class InvalidPropertyException : DomainException<string>
    {
        public InvalidPropertyException(string invalidValue) : base(invalidValue) { }

        public InvalidPropertyException(string invalidValue, string message) : base(invalidValue, message) { }
    }
}

