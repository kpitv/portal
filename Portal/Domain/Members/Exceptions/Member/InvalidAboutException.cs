using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Member
{
    public class InvalidAboutException : DomainException
    {
        public InvalidAboutException(string name) : base(name) { }
    }
}
