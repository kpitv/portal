using System;

namespace Portal.Domain.Shared
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}