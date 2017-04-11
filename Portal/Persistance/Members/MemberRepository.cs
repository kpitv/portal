using System;
using System.Linq;
using Portal.Application.Interfaces;
using Portal.Domain.Members;
using Portal.Persistance.Shared;
using Portal.Application.Shared;
using System.Collections.Generic;

namespace Portal.Persistance.Members
{
    public class MemberRepository : IRepository<Member>
    {
        private readonly DatabaseService databaseService;

        public MemberRepository(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public void Create(Member aggregateRoot)
        {
            databaseService.Members.Add(aggregateRoot.ToMemberEntity());
        }

        public void Delete(Guid id)
        {
            databaseService.Members.Remove(databaseService.Members.Single(a => a.Id == id.ToString()));
        }

        public IEnumerable<Member> Find(Predicate<Member> predicate)
        {
            throw new NotImplementedException();
        }

        public Member Get(Guid id) =>
            databaseService.Members.Single(a => a.Id == id.ToString()).ToMember();

        public IEnumerable<Member> GetAll() =>
            databaseService.Members.ToMappedCollection(EntityMapper.ToMember);

        public void Save()
        {
            databaseService.SaveChanges();
        }

        public void Update(Member aggregateRoot)
        {
            databaseService.Members.Update(aggregateRoot.ToMemberEntity());
        }
    }
}
