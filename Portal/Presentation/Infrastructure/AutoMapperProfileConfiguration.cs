using AutoMapper;
using Portal.Application.Assets.Commands.Models;
using Portal.Application.Members.Commands.Models;
using Portal.Domain.Assets;
using Portal.Domain.Members;
using Portal.Presentation.MVC.Assets.ViewModels;
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
            CreateMap<Member, MemberViewModel>();
            CreateMap<AssetTypeViewModel, CreateAssetTypeModel>();
            CreateMap<AssetTypeViewModel, UpdateAssetTypeModel>();
            CreateMap<AssetType, AssetTypeViewModel>();
        }
    }
}
