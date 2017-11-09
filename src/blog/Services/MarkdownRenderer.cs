using Markdig;
using Microsoft.AspNetCore.Html;

namespace blog.Services
{
    public static class MarkdownRenderer
    {
        private static MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
            .UseDiagrams()
            .UseAdvancedExtensions()
            .UseYamlFrontMatter()
            .DisableHtml()
            .Build();

        public static HtmlString RenderMarkdown(string bodyText)
        {
            var html = Markdown.ToHtml(bodyText ?? string.Empty, pipeline);
            return new HtmlString(html);
        }
    }
}
