using System;
using Portal.Domain.Shared;

namespace Portal.Domain.Members
{
    public class Member : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
    }
}
