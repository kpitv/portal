using System;
using System.Collections.Generic;
using System.Linq;
using Portal.Application.Members.Commands.Models;
using Portal.Domain.Members;

namespace Portal.Application.Members.Commands.Factory
{
    public class MemberFactory : IMemberFactory
    {
        public Member Create(MemberModel model, string id = null)
        {
            if (!Guid.TryParse(id, out Guid memberId) && id != null)
                throw new ArgumentException("Invalid id");

            var memberName = new MemberName(
                new LangSet(model.FirstNameInEnglish, model.FirstNameInRussian, model.FirstNameInUkrainian),
                new LangSet(model.SecondNameInEnglish, model.SecondNameInRussian, model.SecondNameInUkrainian),
                new LangSet(model.LastNameInEnglish, model.LastNameInRussian, model.LastNameInUkrainian)
            );
            var phones = model.PhoneNumbers.Select(number => new Phone(number)).ToList();
            var roles = new List<Role>();
            foreach (string role in model.Roles)
            {
                if (Enum.TryParse(role, true, out Role result))
                    roles.Add(result);
                else
                    throw new ArgumentException($"Role \"{role}\" is not available");
            }

            var member = new Member(model.UserId, memberName, model.Email, phones, roles, memberId);

            if (model.ContactLinks != null)
            {
                var contactLinks = new Dictionary<ContactLink, string>();
                foreach (var contact in model.ContactLinks)
                {
                    if (Enum.TryParse(contact.Key, true, out ContactLink result))
                        contactLinks.Add(result, contact.Value);
                    else
                        throw new ArgumentException($"Contact link \"{contact.Key}\" is not available");
                }
                member.Update(contactLinks: contactLinks);
            }

            if (!string.IsNullOrEmpty(model.About))
                member.Update(about: model.About);

            return member;
        }
    }
}
