using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Portal.Domain.Shared;
using System;

namespace Portal.Domain.Users
{
    public sealed class User : IdentityUser<Guid>, IAggregateRoot
    {
        // Entity can be in a non valid state

        #region Properties
        public new Guid Id { get; private set; }
        public new string UserName { get; private set; }
        public Member Member { get; private set; }
        public Guid MemberId { get; private set; }
        #endregion

        #region Methods
        public User(string userName)
        {
            Member member = new Member();
            base.Id = Id = Guid.NewGuid();
            base.UserName = UserName = userName;
            Member = member;
            MemberId = member.Id;
        }
        User() { }
        #endregion
    }
}
