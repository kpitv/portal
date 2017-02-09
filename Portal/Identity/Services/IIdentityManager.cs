using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Portal.Presentation.Identity.Services
{
    public interface IIdentityManager
    {
        UserManager<IdentityUser> User { get; }
        SignInManager<IdentityUser> SignIn { get; }
        RoleManager<IdentityRole> Role{ get; }
        void InitializeUsers();
    }
}