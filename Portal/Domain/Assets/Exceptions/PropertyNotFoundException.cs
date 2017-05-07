using System;
using System.Collections.Generic;
using System.Text;
using Portal.Domain.Shared;

namespace Portal.Domain.Assets.Exceptions
{
    public class PropertyNotFoundException : DomainException<string>
    {
        public PropertyNotFoundException(string invalidValue) : base(invalidValue) { }
    }
}

