using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Portal.Domain.Members;


namespace Portal.Presentation.MVC.Members.ViewModels
{
    public class MemberViewModel
    {
        #region MemberName
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your firstname cant be so long!")]
        public string FirstNameInEnglish { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your firstname cant be so long!")]
        public string FirstNameInRussian { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your firstname cant be so long!")]
        public string FirstNameInUkrainian { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your secondname cant be so long!")]
        public string SecondNameInRussian { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your secondname cant be so long!")]
        public string SecondNameInUkrainian { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your secondname cant be so long!")]
        public string LastNameInEnglish { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your lastname cant be so long!")]
        public string LastNameInRussian { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your lastname cant be so long!")]
        public string LastNameInUkrainian { get; set; }
        #endregion

        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public string SelectedRole { get; set; }
        public string About { get; set; }
        public Dictionary<string, string> ContactLinks { get; set; } = new Dictionary<string, string>();
        public string Vk { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Skype { get; set; }
        public string Telegram { get; set; }
        public string YouTube { get; set; }
        public string PersonalSite { get; set; }
    }
}
