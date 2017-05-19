using System.Collections.Generic;
using Portal.Domain.Shared;

namespace Portal.Application.Interfaces
{
    public interface IValidationService
    {
        Dictionary<ValidationError, string> Errors { get; set; }
        void DomainErrorsHandler(object sender, ValidationEventArgs e);
    }
}
