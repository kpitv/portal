using System;

namespace Portal.Application.Shared
{
    public class PersistanceException : Exception
    {
        public string Method { get; }
        public string EntityName { get; }
        public bool IsStorageException { get; }

        public PersistanceException(string method, string entityName, bool isStorageException = false)
        : base($"{(isStorageException ? "Storage e" : "E")}xception occured while {method} executing with {entityName} entity")
        {
            Method = method;
            EntityName = entityName;
            IsStorageException = isStorageException;
        }
    }
}
