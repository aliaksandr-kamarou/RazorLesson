using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorLesson.Helpers
{
    public static class AuthorNameExtensions
    {
        public static HtmlString AuthorName(this IHtmlHelper helper, string name)
        {
            return new HtmlString($"<h2>Hello {name}</h2>");
        }
    }
}
