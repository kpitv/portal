using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidUserIdException : DomainException<string>
    {
        public InvalidUserIdException(string invalidValue) : base(invalidValue) { }
    }
}
