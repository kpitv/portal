using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.LangSet
{
    public class TextNotEnglishException : DomainException
    {
        public TextNotEnglishException(string name) : base(name) { }
    }
}

