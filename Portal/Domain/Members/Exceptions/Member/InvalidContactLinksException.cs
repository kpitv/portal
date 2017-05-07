using System.Collections.Generic;
using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidContactLinksException : DomainException<Dictionary<ContactLink, string>>
    {
        public InvalidContactLinksException(Dictionary<ContactLink, string> invalidValue) : base(invalidValue) { }
    }
}
