using Portal.Domain.Shared;

namespace Portal.Domain.Members.Exceptions.LangSet
{
    public class TextNotRussianException : DomainException
    {
        public TextNotRussianException(string name) : base(name) { }
    }
}
