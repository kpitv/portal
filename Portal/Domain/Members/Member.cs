using Portal.Domain.Shared;

namespace Portal.Domain.Members
{
    public class Member : AggregateRoot
    {
        public MemberName Name { get; set; }   
    }
}
