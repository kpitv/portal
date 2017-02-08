using Portal.Domain.Users;
using System.Linq;

namespace Portal.Application.Shared
{
    public interface IUserRepository
    {
        #region Queries
        IQueryable<User> GetAll();
        User GetByUserName(string userName);
        #endregion

        #region Commands
        void CreateAsync(User newUser);
        void UpdateAsync(User user); 
        #endregion
    }
}
