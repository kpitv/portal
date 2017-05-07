using System;

namespace Portal.Domain.Shared
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            return !ReferenceEquals(other, null) &&
                (ReferenceEquals(this, other) || GetType() == other.GetType() &&
                (Id != Guid.Empty && other.Id != Guid.Empty && Id == other.Id));
        }

        public override int GetHashCode() =>
            (GetType().ToString() + Id).GetHashCode();

        public static bool operator ==(Entity a, Entity b) =>
            ReferenceEquals(a, null) && ReferenceEquals(b, null) ||
            !ReferenceEquals(a, null) && !ReferenceEquals(b, null) &&
            a.Equals(b);

        public static bool operator !=(Entity a, Entity b) =>
            !(a == b);
    }
}