using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.Phone
{
    public class InvalidPhoneException : DomainException<string>
    {
        public InvalidPhoneException(string invalidValue) : base(invalidValue) { }
    }
}
