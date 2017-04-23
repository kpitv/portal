using System;
using System.Collections.Generic;
using Portal.Application.Interfaces;
using Portal.Domain.Members;

namespace Portal.Application.Members.Queries
{
    public class MemberQueries : IMemberQueries
    {
        private readonly IRepository<Member> repository;

        public MemberQueries(IRepository<Member> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Member> GetMembers() =>
            repository.GetAll();

        public Member GetMember(string id)
        {
            if (!Guid.TryParse(id, out Guid memberId))
                throw  new ArgumentException("Invalid id");
            return repository.Get(memberId);
        }

        public IEnumerable<Member> FindMembers(Predicate<Member> predicate) =>
            repository.Find(predicate);
    }
}
