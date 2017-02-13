using System;
using Portal.Domain.Shared;

namespace Portal.Domain.Members
{
    public class Member : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public MemberName MemberNameEng { get; private set; }
        public MemberName MemberNameRus { get; private set; }
        public MemberName MemberNameUkr { get; private set; }
    }
}
