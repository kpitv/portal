using System.Collections.Generic;
using Portal.Domain.Members;

namespace Portal.Presentation.MVC.Members.ViewModels
{
    public class MembersListViewModel
    {
        public Dictionary<string, Member> Members { get; set; } = new Dictionary<string, Member>();
    }
}
