using Portal.Application.Members.Commands.Models;
using Portal.Domain.Members;

namespace Portal.Application.Members.Commands.Factory
{
    public interface IMemberFactory
    {
        Member Create(MemberModel model, string id = null);
    }
}
