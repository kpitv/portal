using System.Collections.Generic;
using Portal.Domain.Shared;

namespace Portal.Domain.Assets.Exceptions
{
    public class InvalidPropertiesException : DomainException<List<string>>
    {
        public InvalidPropertiesException(List<string> invalidValue) : base(invalidValue) { }
    }
}


