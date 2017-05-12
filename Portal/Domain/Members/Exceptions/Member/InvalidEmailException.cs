using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidEmailException : DomainException
    {
        public InvalidEmailException(string name) : base(name) { }
    }
}
