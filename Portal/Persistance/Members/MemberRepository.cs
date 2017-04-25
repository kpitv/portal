using System;
using System.Linq;
using Portal.Application.Interfaces;
using Portal.Domain.Members;
using Portal.Persistance.Shared;
using Portal.Application.Shared;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Portal.Persistance.Members
{
    public class MemberRepository : IRepository<Member>
    {
        private readonly DatabaseService databaseService;

        public MemberRepository(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        #region Queries
        public IEnumerable<Member> Find(Predicate<Member> predicate) =>
            databaseService.Members.Include(m => m.ContactLinks).Include(m => m.Phones).Include(m => m.Roles)
            .ToMappedCollection(EntityMapper.ToMember).Where(m => predicate(m));

        public Member Get(Guid id) =>
            databaseService.Members
            .Include(m => m.ContactLinks).Include(m => m.Phones).Include(m => m.Roles)
            .Single(a => a.Id == id.ToString()).ToMember();

        public IEnumerable<Member> GetAll() =>
            databaseService.Members.Include(m => m.ContactLinks).Include(m => m.Phones).Include(m => m.Roles)
            .ToMappedCollection(EntityMapper.ToMember);
        #endregion

        #region Commands
        public void Create(Member aggregateRoot)
        {
            databaseService.Members.Add(aggregateRoot.ToMemberEntity());
        }

        public void Update(Member aggregateRoot)
        {
            databaseService.Members.Update(aggregateRoot.ToMemberEntity());
        }

        public void Delete(Guid id)
        {
            databaseService.Members.Remove(databaseService.Members.Single(a => a.Id == id.ToString()));
        }

        public void Save()
        {
            databaseService.SaveChanges();
        }
        #endregion
    }
}
