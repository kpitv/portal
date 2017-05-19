using System;

namespace Portal.Domain.Shared
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public static event EventHandler<ValidationEventArgs> ErrorOccured;

        public void RaiseError(object sender, ValidationEventArgs e)
        {
            ErrorOccured?.Invoke(sender, e);
        }

        public override bool Equals(object obj) =>
            !ReferenceEquals(obj, null) && EqualsCore(obj as T);

        protected abstract bool EqualsCore(T other);

        public override int GetHashCode() =>
            GetHashCodeCore();

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right) =>
            ReferenceEquals(left, null) && ReferenceEquals(right, null) ||
            !ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Equals(right);

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right) =>
            !(left == right);
    }
}