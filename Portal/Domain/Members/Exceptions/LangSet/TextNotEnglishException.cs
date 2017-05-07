using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.LangSet
{
    public class TextNotEnglishException : DomainException<string>
    {
        public TextNotEnglishException(string invalidValue) : base(invalidValue) { }
    }
}

