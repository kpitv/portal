using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Portal.Presentation.MVC.Members.ViewModels;

namespace Portal.Presentation.MVC.Shared.TagHelpers
{
    [HtmlTargetElement("div", Attributes = "server-validation-for")]
    [HtmlTargetElement("span", Attributes = "server-validation-for")]
    public class ServerValidationTagHelper : TagHelper
    {
        private readonly IStringLocalizer<ServerValidationTagHelper> localizer;

        public MemberViewModel Model { get; set; }

        [HtmlAttributeName("server-validation-for")]
        public string Field { get; set; }

        [HtmlAttributeName("server-validation-errors")]
        public ILookup<string, string> Errors { get; set; }

        public ServerValidationTagHelper(IStringLocalizer<ServerValidationTagHelper> localizer)
        {
            this.localizer = localizer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string content = string.Empty;
            var errorsList = Errors.FirstOrDefault(e => e.Key == Field)?.ToList() ?? new List<string>();
            content = errorsList.Aggregate(content, (current, error) => current + $"{localizer[error]}\n");
            content = content != string.Empty ? content.Substring(0, content.Length - 1) : content;
            output.Content.SetContent(content);
        }
    }
}
