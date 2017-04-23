using System;
using System.Collections.Generic;
using Portal.Domain.Members;

namespace Portal.Application.Members.Queries
{
    public interface IMemberQueries
    {
        IEnumerable<Member> GetMembers();
        Member GetMember(string id);
        IEnumerable<Member> FindMembers(Predicate<Member> predicate); 
    }
}
