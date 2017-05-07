using System.Collections.Generic;
using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidPhoneListException : DomainException<List<Members.Phone>>
    {
        public InvalidPhoneListException(List<Members.Phone> invalidValue) : base(invalidValue) { }
    }
}
