using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Portal.Domain.Members;


namespace Portal.Presentation.MVC.Members.ViewModels
{
    public class MemberViewModel
    {
        public ILookup<string, string> Errors { get; set; } =
            new Dictionary<string, string>().ToLookup(e => e.Key, e => e.Value);

        [HiddenInput]
        public string Id { get; set; }

        [HiddenInput]
        public string Username { get; set; }

        #region MemberName
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your first name can't be so long!")]
        public string FirstNameInEnglish { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your first name can't be so long!")]
        public string FirstNameInRussian { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your first name can't be so long!")]
        public string FirstNameInUkrainian { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your second name can't be so long!")]
        public string SecondNameInRussian { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your second name can't be so long!")]
        public string SecondNameInUkrainian { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your second name can't be so long!")]
        public string LastNameInEnglish { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your last name can't be so long!")]
        public string LastNameInRussian { get; set; }
        [Required]
        [StringLength(maximumLength: 30, ErrorMessage = "OMG! Your last name can't be so long!")]
        public string LastNameInUkrainian { get; set; }
        #endregion

        [EmailAddress]
        public string Email { get; set; }
        public List<string> PhoneNumbers { get; set; } = new List<string>();
        public Dictionary<string, bool> Roles { get; set; } =
            Enum.GetNames(typeof(Role)).ToDictionary(c => c, c => false);
        public string About { get; set; }
        public Dictionary<string, string> ContactLinks { get; set; } =
            Enum.GetNames(typeof(ContactLink)).ToDictionary(c => c, c => string.Empty);
    }
}
