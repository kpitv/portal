using System.Collections.Generic;
using Portal.Domain.Shared;

namespace Portal.Domain.Assets.Exceptions
{
    public class InvalidPropertiesException : DomainException
    {
        public InvalidPropertiesException(string name) : base(name) { }
    }
}


