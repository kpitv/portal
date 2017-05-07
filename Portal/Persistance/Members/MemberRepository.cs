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
        public IEnumerable<Member> Find(Predicate<Member> predicate)
        {
            try
            {
                return databaseService.Members.Include(m => m.ContactLinks)
                       .Include(m => m.Phones)
                       .Include(m => m.Roles)
                       .ToMappedCollection(EntityMapper.ToMember)
                       .Where(m => predicate(m));
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Find), nameof(Member));
            }
        }

        public Member Get(Guid id)
        {
            try
            {
                return databaseService.Members
                        .Include(m => m.ContactLinks)
                        .Include(m => m.Phones)
                        .Include(m => m.Roles)
                        .Single(a => a.Id == id.ToString())
                        .ToMember();
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Get), nameof(Member));
            }
        }

        public IEnumerable<Member> GetAll()
        {
            try
            {
                return databaseService.Members.Include(m => m.ContactLinks)
                       .Include(m => m.Phones)
                       .Include(m => m.Roles)
                       .ToMappedCollection(EntityMapper.ToMember);
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(GetAll), nameof(Member));
            }
        }

        #endregion

        #region Commands
        public void Create(Member aggregateRoot)
        {
            try
            {
                databaseService.Members.Add(aggregateRoot.ToMemberEntity());
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Create), nameof(Member));
            }
        }

        public void Update(Member aggregateRoot)
        {
            try
            {
                var memberEntity = aggregateRoot.ToMemberEntity();
                databaseService.Members.Update(memberEntity);
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Update), nameof(Member));
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                databaseService.Members.Remove(databaseService.Members.Single(a => a.Id == id.ToString()));
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Delete), nameof(Member));
            }
        }

        public void Save()
        {
            try
            {
                databaseService.SaveChanges();
            }
            catch (Exception)
            {
                throw new PersistanceException(nameof(Save), nameof(Member), true);
            }
        }

        public void DetachAllEntities()
        {
            databaseService.DetachAllEntities();
        }
        #endregion
    }
}
