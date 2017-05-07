using System.Collections.Generic;
using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidRoleListException : DomainException<List<Role>>
    {
        public InvalidRoleListException(List<Role> invalidValue) : base(invalidValue) { }
    }
}
