using System.Threading.Tasks;

namespace Portal.Application.Users
{
    public interface IUsersQueryService
    {
        Task<bool> SignInAsync(string userName, string password, bool isPersitent = false);
    }
}
