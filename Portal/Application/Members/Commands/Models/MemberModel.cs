using System.Collections.Generic;

namespace Portal.Application.Members.Commands.Models
{
    public class MemberModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        // MemberName
        public string FirstNameInEnglish { get; set; }
        public string FirstNameInRussian { get; set; }
        public string FirstNameInUkrainian { get; set; }
        public string SecondNameInEnglish { get; set; }
        public string SecondNameInRussian { get; set; }
        public string SecondNameInUkrainian { get; set; }
        public string LastNameInEnglish { get; set; }
        public string LastNameInRussian { get; set; }
        public string LastNameInUkrainian { get; set; }
        // Contacts
        public string Email { get; set; }
        public List<string> PhoneNumbers { get; set; } = new List<string>();
        public List<string> Roles { get; set; } = new List<string>();
        public Dictionary<string, string> ContactLinks { get; set; } = new Dictionary<string, string>();
        // Other
        public string About { get; set; }
    }
}
