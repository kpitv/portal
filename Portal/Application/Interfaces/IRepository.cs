using Portal.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Portal.Application.Interfaces
{
    public interface IRepository<T> where T : AggregateRoot
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        IEnumerable<T> Find(Predicate<T> predicate);
        void Create(T aggregateRoot);
        void Update(T aggregateRoot);
        void Delete(Guid id);
        void Save();
        void DetachAllEntities();
    }
}
