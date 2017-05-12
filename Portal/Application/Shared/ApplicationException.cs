using System;
using System.Linq;

namespace Portal.Application.Shared
{
    public class ApplicationException : Exception
    {
        public string Name { get; set; }
        public ApplicationExceptionType Type { get; }
        public ILookup<string, string> Errors { get; }

        public ApplicationException(string name, 
            ApplicationExceptionType type = ApplicationExceptionType.Server)
        {
            Name = name;
            Type = type;
        }

        public ApplicationException(ILookup<string, string> errors)
        {
            Name = "ValidationException";
            Type = ApplicationExceptionType.Validation;
            Errors = errors;
        }
    }

    public enum ApplicationExceptionType
    {
        Server,
        Validation,
        Storage
    }
}
