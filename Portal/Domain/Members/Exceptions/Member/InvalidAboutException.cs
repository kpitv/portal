using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidAboutException : DomainException<string>
    {
        public InvalidAboutException(string invalidValue) : base(invalidValue) { }
    }
}
