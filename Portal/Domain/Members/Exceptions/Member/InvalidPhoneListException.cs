using System.Collections.Generic;
using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidPhoneListException : DomainException
    {
        public InvalidPhoneListException(string name) : base(name) { }
    }
}
