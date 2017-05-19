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
using Portal.Application.Shared;
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
        //private readonly ResourceManager resourceManager;

        public MembersController(IdentityManager manager, IMemberCommands commands,
            IMemberQueries queries, IMapper mapper /*ResourceManager resourceManager*/)
        {
            this.manager = manager;
            this.commands = commands;
            this.queries = queries;
            this.mapper = mapper;
            //this.resourceManager = resourceManager;
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
                IsCurrent = user.Id == currentUser.Id,
                Username = username
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await manager.User.GetUserAsync(HttpContext.User);
            return View(new MemberViewModel
            {
                Email = user.Email
            });
        }

        [HttpPost]
        public IActionResult Create(MemberViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_MemberForm", model);

            var member = mapper.Map<MemberModel>(model);
            member.UserId = manager.User.GetUserId(HttpContext.User);
            member.Roles = model.Roles.Where(r => r.Value).Select(r => r.Key).ToList();
            member.ContactLinks = model.ContactLinks.Where(c => !string.IsNullOrWhiteSpace(c.Value))
                .ToDictionary(c => c.Key, c => c.Value);
            try
            {
                commands.Create(member);
            }
            catch (ApplicationException ex) when (ex.Type == ApplicationExceptionType.Validation)
            {
                model.Errors = ex.Errors;
                return PartialView("_MemberForm", model);
            }
            catch (ApplicationException)
            {
                // redirect to error view
            }
            return RedirectToAction("Profile", new { username = manager.User.GetUserName(HttpContext.User) });
        }

        [HttpGet]
        [Route("Members/Edit/{username}")]
        public async Task<IActionResult> Edit(string username)
        {
            var userId = (await manager.User.FindByNameAsync(username)).Id;
            var member = queries.FindMembers(m => m.UserId == userId).FirstOrDefault();

            if (member != null)
            {
                var memberModel = new MemberViewModel()
                {
                    About = member.About,
                    ContactLinks = Enum.GetNames(typeof(ContactLink))
                        .Select(c => (ContactLink)Enum.Parse(typeof(ContactLink), c))
                        .ToDictionary(c => c.ToString(), c => member.ContactLinks.ContainsKey(c)
                            ? member.ContactLinks[c]
                            : string.Empty),
                    Email = member.Email,
                    FirstNameInEnglish = member.Name.FirstName.InEnglish,
                    FirstNameInRussian = member.Name.FirstName.InRussian,
                    FirstNameInUkrainian = member.Name.FirstName.InUkrainian,
                    SecondNameInRussian = member.Name.SecondName.InRussian,
                    SecondNameInUkrainian = member.Name.SecondName.InUkrainian,
                    LastNameInEnglish = member.Name.LastName.InEnglish,
                    LastNameInRussian = member.Name.LastName.InRussian,
                    LastNameInUkrainian = member.Name.LastName.InUkrainian,
                    Id = member.Id.ToString(),
                    PhoneNumbers = member.Phones.Select(p => p.Number).ToList(),
                    Roles = Enum.GetNames(typeof(Role)).ToDictionary(r => r, r => member.Roles.Contains((Role)Enum.Parse(typeof(Role), r))),
                };
                return View(memberModel);
            }

            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MemberViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_MemberForm", model);

            var member = mapper.Map<MemberModel>(model);
            member.Id = model.Id;
            member.UserId = manager.User.GetUserId(HttpContext.User);
            member.Roles = model.Roles.Where(r => r.Value).Select(r => r.Key).ToList();
            member.ContactLinks = model.ContactLinks.Where(c => !string.IsNullOrWhiteSpace(c.Value))
                .ToDictionary(c => c.Key, c => c.Value);
            try
            {
                commands.Update(member);
            }
            catch (ApplicationException ex) when (ex.Type == ApplicationExceptionType.Validation)
            {
                model.Errors = ex.Errors;
                return PartialView("_MemberForm", model);
            }
            catch (ApplicationException)
            {
                // redirect to error view
            }
            return RedirectToAction("Profile", new { username = manager.User.GetUserName(HttpContext.User) });
        }
    }
}
