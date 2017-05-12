using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Portal.Domain.Shared;
using static Portal.Domain.Shared.ValidationError;

namespace Portal.Domain.Members
{
    public sealed class Member : AggregateRoot
    {
        public static event EventHandler<ValidationEventArgs> ErrorOccurred;

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
            if (userId is null || name is null || email is null ||
                phones is null || phones.Any(p => p is null) || roles is null)
                throw new ArgumentNullException();
            if (contactLinks != null)
                if (contactLinks.Values.Any(c => c is null))
                    throw new ArgumentNullException();

            if (!ValidateProperties(userId, name, email, phones, roles, about, contactLinks))
                throw new ArgumentException();

            UserId = userId;
            Name = name;
            Email = email;
            Phones = phones;
            Roles = roles;
            About = about ?? string.Empty;
            ContactLinks = contactLinks ?? new Dictionary<ContactLink, string>();
        }
        #endregion

        #region Methods

        private bool ValidateProperties(string userId = null, MemberName name = null, string email = null, IReadOnlyCollection<Phone> phones = null,
            IReadOnlyCollection<Role> roles = null, string about = null, IReadOnlyDictionary<ContactLink, string> contactLinks = null)
        {
            var state = true;
            if (userId != null && string.IsNullOrWhiteSpace(userId))
            {
                ErrorOccurred?.Invoke(this, new ValidationEventArgs(InvalidMemberUserId, nameof(UserId)));
                state = false;
            }
            if (email != null && !Regex.IsMatch(email ?? "", @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
            {
                ErrorOccurred?.Invoke(this, new ValidationEventArgs(InvalidMemberEmail, nameof(Email)));
                state = false;
            }
            if (phones != null && phones.Any() && phones.Distinct().Count() == phones.Count)
            {
                ErrorOccurred?.Invoke(this, new ValidationEventArgs(InvalidMemberPhoneList, nameof(Phones)));
                state = false;
            }
            if (roles != null && roles.Any() && roles.Distinct().Count() == roles.Count)
            {
                ErrorOccurred?.Invoke(this, new ValidationEventArgs(InvalidMemberRoleList, nameof(Roles)));
                state = false;
            }
            if (ValidateAbout(about))
            {
                ErrorOccurred?.Invoke(this, new ValidationEventArgs(InvalidMemberAbout, nameof(About)));
                state = false;
            }
            if (contactLinks != null && contactLinks.Count > 0 && contactLinks.Values.Distinct().Count() == contactLinks.Values.Count())
            {
                ErrorOccurred?.Invoke(this, new ValidationEventArgs(InvalidMemberContactLinks, nameof(ContactLinks)));
                state = false;
            }
            return state;
        }

        public static Member CreateWithId(Guid id, string userId, MemberName name, string email,
            List<Phone> phones, List<Role> roles, string about = null,
            Dictionary<ContactLink, string> contactLinks = null) =>
                new Member(userId, name, email, phones, roles, about, contactLinks) { Id = id };

        public void Update(MemberName name = null, string email = null, List<Phone> phones = null,
            List<Role> roles = null, string about = null, Dictionary<ContactLink, string> contactLinks = null)
        {
            if (!ValidateProperties(name: name, email: email, phones: phones, roles: roles,
                about: about, contactLinks: contactLinks))
                throw new ArgumentException();

            Name = name ?? Name;
            Email = email ?? Email;
            Phones = phones ?? Phones;
            Roles = roles ?? Roles;
            About = about ?? About;
            ContactLinks = contactLinks ?? ContactLinks;
        }

        public static bool ValidateAbout(string about, int maxLength = 500) =>
            !string.IsNullOrWhiteSpace(about) && about.Length <= maxLength;
        #endregion
    }
}
