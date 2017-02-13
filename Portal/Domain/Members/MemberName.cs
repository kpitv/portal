using Portal.Domain.Shared;

namespace Portal.Domain.Members
{
    public class MemberName : ValueObject<MemberName>
    {
        public MemberName(LangSet secondName, LangSet lastName, LangSet firstName)
        {
            SecondName = secondName;
            LastName = lastName;
            FirstName = firstName;
        }

        public LangSet FirstName { get; private set; }
        public LangSet SecondName { get; private set; }
        public LangSet LastName { get; private set; }

        





        protected override bool EqualsCore(MemberName other)
        {
            return true;
        }

        protected override int GetHashCodeCore()
        {
            return 0;
        }
    }
}
