using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Presentation.Identity.Users.Models
{
    public class EmailToken
    {
        [Key, EmailAddress]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Email { get; set; }
        public string TokenHash { get; set; }
        public DateTime Created { get; set; }
    }
}
