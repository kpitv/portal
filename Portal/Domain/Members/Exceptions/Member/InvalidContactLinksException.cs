using System.Collections.Generic;
using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidContactLinksException : DomainException
    {
        public InvalidContactLinksException(string name) : base(name) { }
    }
}
