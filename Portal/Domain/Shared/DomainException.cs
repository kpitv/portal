using System;

namespace Portal.Domain.Shared
{
    public class DomainException : Exception
    {
        public string Name { get; }
        public string Field { get; }

        protected DomainException(string name)
        {
            Name = name;
        }

        protected DomainException(string name, string message, string field) : base(message)
        {
            Name = name;
            Field = field;
        }
    }
}
