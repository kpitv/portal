using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.MemberName
{
    public class InvalidFirstNameException : DomainException<Members.LangSet>
    {
        public InvalidFirstNameException(Members.LangSet invalidValue) : base(invalidValue) { }
    }
}
