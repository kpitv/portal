using Portal.Domain.Shared;

namespace Portal.Domain.Assets.Exceptions
{
    public class InvalidPropertyException : DomainException
    {
        public InvalidPropertyException(string name) : base(name) { }

        public InvalidPropertyException(string name, string message) : base(name, message, "") { }
    }
}

