using System;
using System.Collections.Generic;
using System.Linq;
using Portal.Application.Errors;
using Portal.Application.Interfaces;
using Portal.Application.Members.Commands.Models;
using Portal.Domain.Assets.Exceptions;
using Portal.Domain.Members;
using Portal.Domain.Shared;
using static Portal.Application.Errors.ApplicationError;
using static Portal.Domain.Shared.ValidationError;

namespace Portal.Application.Members.Commands.Factory
{
    public class MemberFactory : IMemberFactory
    {
        private readonly IValidationService validation;
        private readonly ErrorService error;
        public MemberFactory(IValidationService validation, ErrorService error)
        {
            this.validation = validation;
            this.error = error;
        }

        public Member Create(MemberModel model, string id = null)
        {
            var state = true;

            if (!Guid.TryParse(id, out Guid memberId) && id != null)
                throw new ArgumentException("Invalid id");

            var memberName = CreateMemberName(model);
            state = memberName != null;

            var phones = new List<Phone>();
            foreach (var phoneNumber in model.PhoneNumbers)
            {
                try
                {
                    phones.Add(new Phone(phoneNumber));
                }
                catch (ArgumentException)
                {
                    state = false;
                }
            }

            var roles = new List<Role>();
            foreach (string role in model.Roles)
            {
                if (Enum.TryParse(role, true, out Role result))
                    roles.Add(result);
                else
                {
                    error.Raise(this, new ErrorEventArgs(InvalidMemberRole, "Roles"));
                    state = false;
                    break;
                }
            }

            var contactLinks = new Dictionary<ContactLink, string>();
            if (model.ContactLinks.Any())
                foreach (var contact in model.ContactLinks)
                    if (Enum.TryParse(contact.Key, true, out ContactLink result))
                        contactLinks.Add(result, contact.Value);
                    else
                    {
                        error.Raise(this, new ErrorEventArgs(InvalidMemberContactLink, "ContactLinks"));
                        state = false;
                    }
            else
                contactLinks = null;

            if (!state)
                throw new ArgumentException();

            var member = id is null
                ? new Member(model.UserId, memberName, model.Email, phones, roles, model.About, contactLinks)
                : Member.CreateWithId(memberId.ToString(), model.UserId, memberName, model.Email, phones, roles, model.About, contactLinks);

            return member;
        }

        private MemberName CreateMemberName(MemberModel model)
        {
            LangSet firstName = null;
            LangSet secondName = null;
            LangSet lastName = null;
            try
            {
                firstName = new LangSet(model.FirstNameInEnglish, model.FirstNameInRussian,
                   model.FirstNameInUkrainian);
            }
            catch (ArgumentException)
            {
                var newErrors = validation.Errors.ToDictionary(e => e.Key, e => e.Value);
                foreach (var item in validation.Errors)
                {
                    var langSetError = item.Key;
                    switch (langSetError)
                    {
                        case TextNotEnglish:
                            newErrors.Remove(langSetError);
                            newErrors.Add(InvalidFirstNameInEnglish, nameof(model.FirstNameInEnglish));
                            break;
                        case TextNotRussian:
                            newErrors.Remove(langSetError);
                            newErrors.Add(InvalidFirstNameInRussian, nameof(model.FirstNameInRussian));
                            break;
                        case TextNotUkrainian:
                            newErrors.Remove(langSetError);
                            newErrors.Add(InvalidFirstNameInUkrainian, nameof(model.FirstNameInUkrainian));
                            break;
                    }
                }
                validation.Errors = newErrors;
            }
            try
            {
                secondName = new LangSet(string.Empty, model.SecondNameInRussian,
                    model.SecondNameInUkrainian);
            }
            catch (ArgumentException)
            {
                var newErrors = validation.Errors.ToDictionary(e => e.Key, e => e.Value);
                foreach (var item in validation.Errors)
                {
                    var langSetError = item.Key;
                    switch (langSetError)
                    {
                        case TextNotRussian:
                            newErrors.Remove(langSetError);
                            newErrors.Add(InvalidSecondNameInRussian, nameof(model.SecondNameInRussian));
                            break;
                        case TextNotUkrainian:
                            newErrors.Remove(langSetError);
                            newErrors.Add(InvalidSecondNameInUkrainian, nameof(model.SecondNameInUkrainian));
                            break;
                    }
                }
                validation.Errors = newErrors;
            }
            try
            {
                lastName = new LangSet(model.LastNameInEnglish, model.LastNameInRussian,
                    model.LastNameInUkrainian);
            }
            catch (ArgumentException)
            {
                var newErrors = validation.Errors.ToDictionary(e => e.Key, e => e.Value);
                foreach (var item in validation.Errors)
                {
                    var langSetError = item.Key;
                    switch (langSetError)
                    {
                        case TextNotEnglish:
                            newErrors.Remove(langSetError);
                            newErrors.Add(InvalidLastNameInEnglish, nameof(model.LastNameInEnglish));
                            break;
                        case TextNotRussian:
                            newErrors.Remove(langSetError);
                            newErrors.Add(InvalidLastNameInRussian, nameof(model.LastNameInRussian));
                            break;
                        case TextNotUkrainian:
                            newErrors.Remove(langSetError);
                            newErrors.Add(InvalidLastNameInUkrainian, nameof(model.LastNameInUkrainian));
                            break;
                    }
                }
                validation.Errors = newErrors;
            }
            return firstName is null || secondName is null || lastName is null
                ? null
                : new MemberName(firstName, secondName, lastName);
        }
    }
}
