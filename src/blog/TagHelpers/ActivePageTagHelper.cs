using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;

namespace blog.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "highlight-active")]
    public class RouteTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private IUrlHelperFactory _urlHelper { get; set; }

        public RouteTagHelper(IUrlHelperFactory urlHelper)
        {
            _urlHelper = urlHelper;
        }

        [HtmlAttributeName("css-active-class")]
        public string CssClass { get; set; } = "active";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Remove(output.Attributes["highlight-active"]);

            var urlHelper = _urlHelper.GetUrlHelper(ViewContext);

            var url = output.Attributes["href"].Value.ToString();

            if (urlHelper.Action() == url)
            {
                var linkTag = new TagBuilder("a");
                linkTag.Attributes.Add("class", this.CssClass);
                output.MergeAttributes(linkTag);
            }
        }
    }
}
