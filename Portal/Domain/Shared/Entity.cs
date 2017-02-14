﻿using System;

namespace Portal.Domain.Shared
{
    public abstract class Entity
    {
        public virtual Guid Id { get; }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == Guid.Empty || other.Id == Guid.Empty)
                return false;

            return Id == other.Id;
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