using System;
using Portal.Domain.Shared;

namespace Portal.Domain.Users
{
    public class Member : IEntity
    {
        public Guid Id { get; private set; }
        public Member()
        {
            Id = Guid.NewGuid();
        }
    }
}
