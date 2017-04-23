using System;
using Portal.Application.Interfaces;
using Portal.Application.Members.Commands.Factory;
using Portal.Application.Members.Commands.Models;
using Portal.Domain.Members;

namespace Portal.Application.Members.Commands
{
    public class MemberCommands : IMemberCommands
    {
        private readonly IRepository<Member> repository;
        private readonly IMemberFactory factory;

        public MemberCommands(IRepository<Member> repository, IMemberFactory factory)
        {
            this.repository = repository;
            this.factory = factory;
        }

        public void Create(MemberModel model)
        {
            repository.Create(factory.Create(model));
            repository.Save();
        }

        public void Update(MemberModel model)
        {
            repository.Update(factory.Create(model, model.Id));
            repository.Save();
        }

        public void Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid memberId))
                throw new ArgumentException("Invalid id");

            repository.Delete(memberId);
            repository.Save();
        }
    }
}
