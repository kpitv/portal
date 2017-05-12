using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.MemberName
{
    public class InvalidLastNameException : DomainException
    {
        public InvalidLastNameException(string name) : base(name) { }
    }
}

