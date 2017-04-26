using Portal.Application.Members.Commands.Models;

namespace Portal.Application.Members.Commands
{
   public interface IMemberCommands
   {
       void Create(MemberModel model);
       void Update(MemberModel model);
       void Delete(string id);
       void DetachAllEntities();
   }
}
