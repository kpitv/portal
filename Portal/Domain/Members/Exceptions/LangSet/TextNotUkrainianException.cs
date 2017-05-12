using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.LangSet
{
    public class TextNotUkrainianException : DomainException
    {
        public TextNotUkrainianException(string name) : base(name) { }
    }
}
