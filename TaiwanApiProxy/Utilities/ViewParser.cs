namespace TaiwanApiProxy.Utilities
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using TaiwanApiProxy.Controllers;

    /// <summary>
    /// View Parser.
    /// </summary>
    public static class ViewParser
    {
        public static HttpResponseMessage Get(string controllerName, string viewName, object model, ControllerBase controller)
        {
            var contextBase = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var razorViewEngine = new RazorViewEngine();
            var writer = new StringWriter();

            routeData.Values.Add("controller", controllerName);

            var controllerContext = new ControllerContext(contextBase, routeData, controller);
            var razorViewResult = razorViewEngine.FindView(controllerContext, viewName, string.Empty, false);
            var viewContext = new ViewContext(
                                            controllerContext,
                                            razorViewResult.View,
                                            new ViewDataDictionary(model),
                                            new TempDataDictionary(),
                                            writer);

            razorViewResult.View.Render(viewContext, writer);
            response.Content = new StringContent(writer.ToString());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }
    }
}