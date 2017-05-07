using System;

namespace Portal.Domain.Shared
{
    public class DomainException<T> : Exception
    {
        public T InvalidValue { get; }

        public DomainException(T invalidValue)
        {
            InvalidValue = invalidValue;
        }

        public DomainException(T invalidValue, string message) : base(message)
        {
            InvalidValue = invalidValue;
        }
    }
}
