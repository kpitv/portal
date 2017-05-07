using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.LangSet
{
    public class TextNotUkrainianException : DomainException<string>
    {
        public TextNotUkrainianException(string invalidValue) : base(invalidValue) { }
    }
}
