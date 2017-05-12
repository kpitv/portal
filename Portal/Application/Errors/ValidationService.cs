using System.Collections.Generic;
using Portal.Application.Interfaces;
using Portal.Domain.Shared;

namespace Portal.Application.Errors
{
    public class ValidationService : IValidationService
    {
        public Dictionary<ValidationError, string> Errors { get; } = new Dictionary<ValidationError, string>();

        public void DomainErrorsHandler(object sender, ValidationEventArgs e)
        {
            Errors.Add(e.Name, e.Property);
        }
    }
}
