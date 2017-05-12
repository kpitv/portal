using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Phone
{
    public class InvalidPhoneException : DomainException
    {
        public InvalidPhoneException(string name) : base(name) { }
    }
}
