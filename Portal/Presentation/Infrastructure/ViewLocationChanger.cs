using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

namespace Portal.Presentation.Infrastructure
{
    public class ViewLocationChanger : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return new[]
            {
                "~/MVC/{1}/Views/{0}.cshtml",
                "~/MVC/Shared/Views/{0}.cshtml",
                "~/MVC/Shared/Views/Components/{0}.cshtml"
            };
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
