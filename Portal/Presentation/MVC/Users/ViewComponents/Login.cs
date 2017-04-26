using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Members.Queries;
using Portal.Presentation.Identity.Users;
using Portal.Presentation.MVC.Users.ViewModels;

namespace Portal.Presentation.MVC.Users.ViewComponents
{
    public class Login : ViewComponent
    {
        private readonly IdentityManager manager;
        private readonly IMemberQueries queries;

        public Login(IdentityManager manager, IMemberQueries queries)
        {
            this.manager = manager;
            this.queries = queries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await manager.User.GetUserAsync(HttpContext.User);
            if (user == null) return View(new LoginViewModel { IsLogedIn = false });
            var member = queries.FindMembers(m => m.UserId == user.Id).FirstOrDefault();
            string name = member is null ? user.UserName : member.Name.FirstName.InEnglish;
            return View(new LoginViewModel
            {
                IsLogedIn = true,
                Username = user.UserName,
                DisplayName = name
            });
        }
    }
}
