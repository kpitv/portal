using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.MemberName
{
    public class InvalidLastNameException : DomainException<Members.LangSet>
    {
        public InvalidLastNameException(Members.LangSet invalidValue) : base(invalidValue) { }
    }
}

