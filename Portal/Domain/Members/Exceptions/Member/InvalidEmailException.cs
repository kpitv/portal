using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidEmailException : DomainException<string>
    {
        public InvalidEmailException(string invalidValue) : base(invalidValue) { }
    }
}
