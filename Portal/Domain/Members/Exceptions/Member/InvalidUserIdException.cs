using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidUserIdException : DomainException
    {
        public InvalidUserIdException(string name) : base(name) { }
    }
}
