namespace Minate.Extensions
{
    using System;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;

    public static class HtmlHelperExtensions
    {
        internal const string SessionKeyPrefix = "__MinateCaptcha:";

        public static MvcHtmlString MenuLink(this HtmlHelper html,
                                       string text,
                                       string controller,
                                       string action = "Index",
                                       object routeValues = null,
                                       object htmlAttributes = null,
                                       string currentClass = "current")
        {
            var attributes = new RouteValueDictionary(htmlAttributes);

            var currentController = html.ViewContext.RouteData.Values["controller"] as string ?? "Home";
            var currentAction = html.ViewContext.RouteData.Values["action"] as string ?? "Index";

            var page = string.Format("{0}:{1}", currentController, currentAction).ToLower();
            var thisPage = string.Format("{0}:{1}", controller, action).ToLower();

            attributes["class"] = (string.Equals(page, thisPage) || string.Equals(currentController, controller))
                                      ? currentClass
                                      : "";

            return html.ActionLink(text, action, controller, new RouteValueDictionary(routeValues), attributes);
        }

        public static string PageLinks(this HtmlHelper html, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();

            for(var i = 1; i <= totalPages; ++i)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if(i == currentPage)
                    tag.AddCssClass("current");
                result.Append(tag.ToString());
            }

            return result.ToString();
        }

        public static string Captcha(this HtmlHelper html, string name)
        {
            var challengeGuid = Guid.NewGuid().ToString();
            var session = html.ViewContext.HttpContext.Session;
            session[SessionKeyPrefix + challengeGuid] = RandomSolution();

            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var url = urlHelper.Action("Captcha", "Security", new {challengeGuid});

            return string.Format("<img src = \"{0}\" />", url) + html.Hidden(name, challengeGuid);
        }

        private static string RandomSolution()
        {
            var rng = new Random();
            var buffer = new char[rng.Next(5, 10) + 1];

            for (var i = 0; i < buffer.Length - 1; ++i)
                buffer[i] = (char) ('a' + rng.Next(26));
            buffer[buffer.Length - 1] = (char) ('0' + rng.Next(9));

            return new string(buffer);
        }
    }
}