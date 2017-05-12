using System;
using System.Collections.Generic;

namespace Portal.Application.Errors
{
    public class ErrorService
    {
        public event EventHandler<ErrorEventArgs> ErrorOccurred;
        public Dictionary<ApplicationError, string> Errors { get; } = new Dictionary<ApplicationError, string>();

        public void Raise(object sender, ErrorEventArgs e)
        {
            ErrorOccurred?.Invoke(sender, e);
        }

        public void ErrorsHandler(object sender, ErrorEventArgs e)
        {
            Errors.Add(e.Name, e.Property);
        }
    }
}
