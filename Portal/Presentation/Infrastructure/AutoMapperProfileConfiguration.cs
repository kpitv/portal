using AutoMapper;
using Portal.Application.Members.Commands.Models;
using Portal.Presentation.MVC.Members.ViewModels;

namespace Portal.Presentation.Infrastructure
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
            : this("MyProfile")
        {
        }
        public AutoMapperProfileConfiguration(string profileName)
            : base(profileName)
        {
            CreateMap<MemberViewModel, MemberModel>();
        }
    }
}
