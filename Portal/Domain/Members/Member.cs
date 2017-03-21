using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Portal.Domain.Shared;

namespace Portal.Domain.Members
{
    public class Member : AggregateRoot
    {
        #region Properties
        public string UserId { get; private set; }
        public MemberName Name { get; private set; }
        public string Email { get; private set; }
        public List<Phone> Phones { get; private set; }
        public List<Role> Roles { get; private set; }
        // allow nulls
        public Dictionary<ContactLink, string> ContactLinks { get; private set; } 
        public string About { get; private set; }
        #endregion

        #region Ctors
        public Member(string userId, MemberName name, string email, List<Phone> phones,
            List<Role> roles, string about = null, Dictionary<ContactLink, string> contactLinks = null)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (phones == null || phones.Count == 0) throw new ArgumentNullException(nameof(phones));
            if (roles == null || roles.Count == 0) throw new ArgumentNullException(nameof(roles));
            if (contactLinks?.Count == 0) throw new ArgumentNullException(nameof(contactLinks));

            if (ValidateEmail(email))
                Email = email;
            else throw new ArgumentException(nameof(email));
            if (ValidateAbout(about))
                About = about;
            else throw new ArgumentException(nameof(about));

            UserId = userId;
            Name = name;
            Phones = phones;
            About = about;
            Roles = roles;
            ContactLinks = contactLinks;
        } 
        #endregion

        #region Methods
        public void Update(MemberName name = null, string email = null, List<Phone> phones = null,
            List<Role> roles = null, string about = null, Dictionary<ContactLink, string> contactLinks = null)
        {
            if (phones?.Count == 0)
                throw new ArgumentException(nameof(phones));
            if (roles?.Count == 0)
                throw new ArgumentException(nameof(roles));
            if (contactLinks?.Count == 0)
                ContactLinks = null;

            if (email?.Length == 0)
                Email = null;
            else if (ValidateEmail(email))
                Email = email;
            else throw new ArgumentException(nameof(email));

            if (about?.Length == 0)
                About = null;
            else if (ValidateAbout(about))
                About = about;
            else throw new ArgumentException(nameof(about));

            Name = name ?? Name;
            Phones = phones ?? Phones;
            Roles = roles;
            ContactLinks = contactLinks ?? ContactLinks;
        }
        public static bool ValidateEmail(string email) =>
             Regex.IsMatch(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        public static bool ValidateAbout(string about, int maxLength = 500) =>
             about.Length > 0 && about.Length <= maxLength;
        #endregion
    }
}
