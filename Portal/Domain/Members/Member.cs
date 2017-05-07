using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Portal.Domain.Members.Exceptions.Member;
using Portal.Domain.Shared;

namespace Portal.Domain.Members
{
    public sealed class Member : AggregateRoot
    {
        #region Properties
        public string UserId { get; }
        public MemberName Name { get; private set; }
        public string Email { get; private set; }
        public List<Phone> Phones { get; private set; }
        public List<Role> Roles { get; private set; }

        // can be empty
        public string About { get; private set; }
        public Dictionary<ContactLink, string> ContactLinks { get; private set; }
        #endregion

        #region Ctors
        public Member(string userId, MemberName name, string email, List<Phone> phones, List<Role> roles,
            string about = null, Dictionary<ContactLink, string> contactLinks = null)
        {
            UserId = !string.IsNullOrWhiteSpace(userId) ? userId : throw new InvalidUserIdException(userId);
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = ValidateEmail(email) ? email : throw new InvalidEmailException(email);
            Phones = ValidatePhoneList(phones) ? phones : throw new InvalidPhoneListException(phones);
            Roles = ValidateRoleList(roles) ? roles : throw new InvalidRoleListException(roles);

            About = about != null ? (ValidateAbout(about) ? about : throw new InvalidAboutException(about)) : "";
            ContactLinks = contactLinks != null
                ? (ValidateContactLinks(contactLinks)
                    ? contactLinks
                    : throw new InvalidContactLinksException(contactLinks))
                : new Dictionary<ContactLink, string>();
        }
        #endregion

        #region Methods

        public static Member CreateWithId(Guid id, string userId, MemberName name, string email,
            List<Phone> phones, List<Role> roles, string about = null,
            Dictionary<ContactLink, string> contactLinks = null) =>
                new Member(userId, name, email, phones, roles, about, contactLinks) { Id = id };

        public void Update(MemberName name = null, string email = null, List<Phone> phones = null,
            List<Role> roles = null, string about = null, Dictionary<ContactLink, string> contactLinks = null)
        {
            if (phones?.Count == 0)
                throw new ArgumentException(nameof(phones));
            if (roles?.Count == 0)
                throw new ArgumentException(nameof(roles));
            if (contactLinks?.Count == 0)
                ContactLinks = null;

            if (email != null)
                if (ValidateEmail(email))
                    Email = email;
                else throw new ArgumentException(nameof(email));

            if (about?.Length == 0)
                About = null;
            if (about != null)
                if (ValidateAbout(about))
                    About = about;
                else throw new ArgumentException(nameof(about));

            Name = name ?? Name;
            Phones = phones ?? Phones;
            Roles = roles ?? Roles;
            ContactLinks = contactLinks ?? ContactLinks;
        }

        public static bool ValidateEmail(string email) =>
            Regex.IsMatch(email ?? "", @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

        public static bool ValidateAbout(string about, int maxLength = 500) =>
            !string.IsNullOrWhiteSpace(about) && about.Length <= maxLength;

        public static bool ValidatePhoneList(List<Phone> phones) =>
            phones != null && phones.Any() && !phones.Any(p => p is null) && phones.Distinct().Count() == phones.Count;

        public static bool ValidateRoleList(List<Role> roles) =>
            roles != null && roles.Any() && roles.Distinct().Count() == roles.Count;

        public static bool ValidateContactLinks(Dictionary<ContactLink, string> contactLinks) =>
            contactLinks != null &&
            contactLinks.Count > 0 &&
            !contactLinks.Values.Any(c => c is null) &&
            contactLinks.Values.Distinct().Count() == contactLinks.Values.Count;
        #endregion
    }
}
