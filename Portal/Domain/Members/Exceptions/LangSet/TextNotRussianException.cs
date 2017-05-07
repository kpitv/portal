using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.LangSet
{
    public class TextNotRussianException : DomainException<string>
    {
        public TextNotRussianException(string invalidValue) : base(invalidValue) { }
    }
}
