using Portal.Domain.Shared;

namespace Portal.Domain.Assets.Exceptions
{
    public class PropertyNotFoundException : DomainException
    {
        public PropertyNotFoundException(string name) : base(name) { }
    }
}

