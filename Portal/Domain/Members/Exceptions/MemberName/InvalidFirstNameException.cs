using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.MemberName
{
    public class InvalidFirstNameException : DomainException
    {
        public InvalidFirstNameException(string name) : base(name) { }
    }
}
