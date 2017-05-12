using System.Collections.Generic;
using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidRoleListException : DomainException
    {
        public InvalidRoleListException(string name) : base(name) { }
    }
}
