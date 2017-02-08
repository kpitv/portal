using Microsoft.AspNetCore.Identity;
using Portal.Application.Shared;
using Portal.Domain.Users;
using System.Linq;

namespace Portal.Persistance.Users
{
    public class UserRepository : IUserRepository
    {
        readonly UserManager<User> userManager;

        public UserRepository(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        #region Queries
        public IQueryable<User> GetAll() =>
            userManager.Users;
        public User GetByUserName(string userName) =>
            userManager.Users.Single(u => u.UserName == userName);
        #endregion
        #region Commands
        public void CreateAsync(User newUser)
        {
            userManager.CreateAsync(newUser);
        }
        public void UpdateAsync(User user)
        {
            userManager.UpdateAsync(user);
        } 
        #endregion
    }
}
