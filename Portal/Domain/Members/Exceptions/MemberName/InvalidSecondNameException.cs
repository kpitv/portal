using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.MemberName
{
    public class InvalidSecondNameException : DomainException
    {
        public InvalidSecondNameException(string name) : base(name) { }
    }
}
