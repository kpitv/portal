using System.Collections.Generic;

namespace Portal.Persistance.Members.Entities
{
    public class MemberEntity
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
        //
        public string Email { get; set; }
        public List<PhoneEntity> Phones { get; set; }
        public List<RoleEntity> Roles { get; set; }
        public List<ContactLinkEntity> ContactLinks { get; set; }
        public string About { get; set; }
    }
}
