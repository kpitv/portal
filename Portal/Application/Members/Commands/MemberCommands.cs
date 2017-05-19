using System;
using System.Linq;
using Portal.Application.Errors;
using Portal.Application.Interfaces;
using Portal.Application.Members.Commands.Factory;
using Portal.Application.Members.Commands.Models;
using Portal.Application.Shared;
using Portal.Domain.Members;
using Portal.Domain.Shared;

namespace Portal.Application.Members.Commands
{
    public class MemberCommands : IMemberCommands
    {
        private readonly IRepository<Member> repository;
        private readonly IMemberFactory factory;
        private readonly IValidationService validation;
        private readonly ErrorService error;

        public MemberCommands(IRepository<Member> repository, IMemberFactory factory,
            IValidationService validation, ErrorService error)
        {
            this.repository = repository;
            this.factory = factory;
            this.validation = validation;
            this.error = error;

            Member.ErrorOccurred += validation.DomainErrorsHandler;
            error.ErrorOccurred += error.ErrorsHandler;
            Phone.ErrorOccured += validation.DomainErrorsHandler;
            LangSet.ErrorOccured += validation.DomainErrorsHandler;
            MemberName.ErrorOccured += validation.DomainErrorsHandler;
        }

        public void Create(MemberModel model)
        {
            try
            {
                var member = factory.Create(model);
                repository.Create(member);
                repository.Save();
            }
            catch (ArgumentNullException)
            {
                throw new ApplicationException(nameof(ArgumentNullException));
            }
            catch (ArgumentException)
            {
                var domainErrors = validation.Errors.ToLookup(e => e.Value, e => e.Key.ToString());
                var applicationErrors = error.Errors.ToLookup(e => e.Value, e => e.Key.ToString());

                throw new ApplicationException(domainErrors
                    .Concat(applicationErrors)
                    .SelectMany(errors => errors.Select(value => new { errors.Key, value }))
                    .ToLookup(e => e.Key, e => e.value));
            }
            catch (PersistanceException ex)
            {
                throw new ApplicationException(ex.EntityName, ApplicationExceptionType.Storage);
            }
        }

        public void Update(MemberModel model)
        {
            try
            {
                var member = factory.Create(model, model.Id);
                repository.Update(member);
                repository.Save();
            }
            catch (ArgumentNullException)
            {
                throw new ApplicationException(nameof(ArgumentNullException));
            }
            catch (ArgumentException)
            {
                var domainErrors = validation.Errors.ToLookup(e => e.Value, e => e.Key.ToString());
                var applicationErrors = error.Errors.ToLookup(e => e.Value, e => e.Key.ToString());

                throw new ApplicationException(domainErrors
                    .Concat(applicationErrors)
                    .SelectMany(errors => errors.Select(value => new { errors.Key, value }))
                    .ToLookup(e => e.Key, e => e.value));
            }
            catch (PersistanceException ex) when (ex.IsStorageException)
            {
                throw new ApplicationException("StorageException", ApplicationExceptionType.Server);
            }
            catch (PersistanceException ex)
            {
                throw new ApplicationException(ex.EntityName, ApplicationExceptionType.Storage);
            }
        }

        public void Delete(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid memberId))
                    throw new ArgumentException("Invalid id");

                repository.Delete(memberId);
                repository.Save();
            }
            catch (DomainException ex)
            {
                throw new ApplicationException(ex.Name);
            }
            catch (PersistanceException ex) when (ex.IsStorageException)
            {
                throw new ApplicationException("StorageException", ApplicationExceptionType.Server);
            }
            catch (PersistanceException ex)
            {
                throw new ApplicationException(ex.EntityName);
            }
        }

        public void DetachAllEntities()
        {
            repository.DetachAllEntities();
        }
    }
}
