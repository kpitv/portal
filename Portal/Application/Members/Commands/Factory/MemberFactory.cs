using System;
using System.Collections.Generic;
using System.Linq;
using Portal.Application.Errors;
using Portal.Application.Members.Commands.Models;
using Portal.Domain.Members;
using static Portal.Application.Errors.ApplicationError;

namespace Portal.Application.Members.Commands.Factory
{
    public class MemberFactory : IMemberFactory
    {
        private readonly ErrorService error;
        public MemberFactory(ErrorService error)
        {
            this.error = error;
        }

        public Member Create(MemberModel model, string id = null)
        {
            var state = true;

            if (!Guid.TryParse(id, out Guid memberId) && id != null)
                throw new ArgumentException("Invalid id");

            var memberName = new MemberName(
                new LangSet(model.FirstNameInEnglish, model.FirstNameInRussian, model.FirstNameInUkrainian),
                new LangSet("", model.SecondNameInRussian, model.SecondNameInUkrainian),
                new LangSet(model.LastNameInEnglish, model.LastNameInRussian, model.LastNameInUkrainian)
            );
            var phones = model.PhoneNumbers.Select(number => new Phone(number)).ToList();
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

            var member = id is null
                ? new Member(model.UserId, memberName, model.Email, phones, roles)
                : Member.CreateWithId(memberId, model.UserId, memberName, model.Email, phones, roles);

            if (model.ContactLinks.Any())
            {
                var contactLinks = new Dictionary<ContactLink, string>();
                foreach (var contact in model.ContactLinks)
                {
                    if (Enum.TryParse(contact.Key, true, out ContactLink result))
                        contactLinks.Add(result, contact.Value);
                    else
                    {
                        error.Raise(this, new ErrorEventArgs(InvalidMemberContactLink, "ContactLinks"));
                        state = false;
                    }
                }
                member.Update(contactLinks: contactLinks);
            }

            if (!string.IsNullOrEmpty(model.About))
                member.Update(about: model.About);

            if (!state)
                throw new ArgumentException();

            return member;
        }
    }
}
