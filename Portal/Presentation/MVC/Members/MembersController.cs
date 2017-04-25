using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Members.Commands;
using Portal.Application.Members.Commands.Models;
using Portal.Application.Members.Queries;
using Portal.Domain.Members;
using Portal.Presentation.Identity.Users;
using Portal.Presentation.MVC.Members.ViewModels;

namespace Portal.Presentation.MVC.Members
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly IdentityManager manager;
        private readonly IMemberCommands commands;
        private readonly IMemberQueries queries;
        private readonly IMapper mapper;

        public MembersController(IdentityManager manager, IMemberCommands commands,
            IMemberQueries queries, IMapper mapper)
        {
            this.manager = manager;
            this.commands = commands;
            this.queries = queries;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var members = queries.GetMembers();
            var membersWithUserNames = new Dictionary<string, Member>();
            foreach (var member in members)
            {
                var user = await manager.User.FindByIdAsync(member.UserId);
                membersWithUserNames.Add(user.UserName, member);
            }
            return View(membersWithUserNames);
        }

        [HttpGet]
        [Route("Members/Profile/{username}")]
        public async Task<IActionResult> Profile(string username)
        {
            var user = await manager.User.FindByNameAsync(username);
            var currentUser = await manager.User.GetUserAsync(HttpContext.User);
            if (user is null)
                return NotFound();
            var member = queries.FindMembers(m => m.UserId == user.Id).FirstOrDefault();
            if (member is null)
                if (user.Id == currentUser.Id)
                    RedirectToAction("Create");
                else
                    return NotFound();

            var model = new ProfileViewModel
            {
                Member = member,
                IsCurrent = user.Id == currentUser.Id
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await manager.User.GetUserAsync(HttpContext.User);
            queries.FindMembers(m => m.UserId == user.Id);
            if (queries.FindMembers(m => m.UserId == user.Id).Any())
                return NotFound();
            var model = new MemberViewModel { Email = user.Email };
            foreach (string item in Enum.GetNames(typeof(Role)))
                model.Roles.Add(item);
            foreach (string item in Enum.GetNames(typeof(ContactLink)))
                model.ContactLinks.Add(item, "");

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(MemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (string item in Enum.GetNames(typeof(Role)))
                    model.Roles.Add(item);
                foreach (string item in Enum.GetNames(typeof(ContactLink)))
                    model.ContactLinks.Add(item, "");
                return View(model);
            }
            try
            {
                var member = mapper.Map<MemberModel>(model);
                member.UserId = manager.User.GetUserId(HttpContext.User);
                member.Roles.Add(model.SelectedRole);
                member.PhoneNumbers.Add(model.PhoneNumber);

                if (!string.IsNullOrEmpty(model.Vk))
                    member.ContactLinks.Add("Vk", model.Vk);
                if (!string.IsNullOrEmpty(model.Facebook))
                    member.ContactLinks.Add("Facebook", model.Facebook);
                if (!string.IsNullOrEmpty(model.Instagram))
                    member.ContactLinks.Add("Instagram", model.Instagram);

                commands.Create(member);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                foreach (string item in Enum.GetNames(typeof(Role)))
                    model.Roles.Add(item);
                foreach (string item in Enum.GetNames(typeof(ContactLink)))
                    model.ContactLinks.Add(item, "");
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
