namespace TaiwanApiProxy.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Entities;
    using TaiwanApiProxy.Utilities;
    using TaiwanApiProxy.ViewModels;

    public class ProxyGetController : ApiController
    {
        [AcceptVerbs("GET")]
        public HttpResponseMessage List()
        {
            var methods = typeof(ProxyPostController).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            var actionApis = new List<ApiAction>();

            foreach (var method in methods)
            {
                actionApis.Add(GetAction(method));
            }

            return ViewParser.Get("Help", "List", actionApis, new HelpController());
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage Detail()
        {
            var request = HttpContext.Current.Request;
            var apiAction = request.RequestContext.RouteData.Values["apiaction"].ToString();
            var help = string.IsNullOrEmpty(request.QueryString["help"]) ? 0 : Convert.ToInt32(request.QueryString["help"]);
            var methodInfos = typeof(ProxyPostController).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                                    .Where(m => m.Name.Equals(apiAction, StringComparison.CurrentCultureIgnoreCase));

            var viewModel = new HelpActionViewModel
            {
                Help = help,
                Action = apiAction,
                Parameters = ActionExtension.GetParameters(methodInfos, help).ToList()
            };

            return ViewParser.Get("Help", "Detail", viewModel, new HelpController());
        }

        private ApiAction GetAction(MethodInfo method)
        {
            var attribute = method.GetCustomAttributes(typeof(QueryStringAttribute), false).FirstOrDefault();

            if (attribute != null)
            {
                return new ApiAction
                {
                    Name = method.Name,
                    Help = ((QueryStringAttribute)attribute).Help
                };
            }
            else
            {
                return new ApiAction
                {
                    Name = method.Name
                };
            }
        }
    }
}
