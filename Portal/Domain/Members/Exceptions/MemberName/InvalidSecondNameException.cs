using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.MemberName
{
    public class InvalidSecondNameException : DomainException<Members.LangSet>
    {
        public InvalidSecondNameException(Members.LangSet invalidValue) : base(invalidValue) { }
    }
}
