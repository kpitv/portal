using Portal.Domain.Members;

namespace Portal.Presentation.MVC.Members.ViewModels
{
    public class ProfileViewModel
    {
        public Member Member { get; set; }
        public string Username { get; set; }
        public bool IsCurrent { get; set; }
    }
}
