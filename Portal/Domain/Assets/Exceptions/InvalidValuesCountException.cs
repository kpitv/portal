using Portal.Domain.Shared;

namespace Portal.Domain.Assets.Exceptions
{
    public class InvalidValuesCountException : DomainException
    {
        public InvalidValuesCountException(string name) : base(name) { }
    }
}
