using Microsoft.AspNetCore.Identity;
using Portal.Application.Shared;
using Portal.Domain.Users;
using System.Threading.Tasks;

namespace Portal.Application.Users
{
    public class UsersQueryService : IUsersQueryService
    {
        readonly SignInManager<User> signInManager;
        readonly IUserRepository userRepository;

        public UsersQueryService(SignInManager<User> signInManager, IUserRepository userRepository)
        {
            this.signInManager = signInManager;
            this.userRepository = userRepository;
        }
        public async Task<bool> SignInAsync(string userName, string password, bool isPersitent = false)
        {  
            var result = await signInManager.PasswordSignInAsync(userName, password, isPersitent, lockoutOnFailure: false);
            return result.Succeeded;
        }
    }
}
