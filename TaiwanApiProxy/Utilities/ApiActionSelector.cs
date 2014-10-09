namespace TaiwanApiProxy.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using TaiwanApiProxy.Attributes;

    public class ApiActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            object actionName;
            var hasActionName = controllerContext.RouteData.Values.TryGetValue("action", out actionName);

            var method = controllerContext.Request.Method;
            var allMethods = controllerContext.ControllerDescriptor.ControllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            var validMethods = Array.FindAll(allMethods, IsValidActionMethod);
            var actionDescriptors = new HashSet<ReflectedHttpActionDescriptor>();
            var queryStrings = HttpUtility.ParseQueryString(controllerContext.Request.RequestUri.Query);
            
            foreach (var actionDescriptor in validMethods.Select(m => new ReflectedHttpActionDescriptor(controllerContext.ControllerDescriptor, m)))
            {
                actionDescriptors.Add(actionDescriptor);
            }

            IEnumerable<ReflectedHttpActionDescriptor> foundActions;

            if (hasActionName)
            {
                foundActions =
                            actionDescriptors.Where(
                                i =>
                                i.ActionName.ToLowerInvariant() == actionName.ToString().ToLowerInvariant() &&
                                i.SupportedHttpMethods.Contains(method)).ToArray();
            }
            else
            {
                foundActions = 
                            actionDescriptors.Where(
                                i => 
                                i.ActionName.ToLowerInvariant().Contains(method.ToString().ToLowerInvariant()) && 
                                i.SupportedHttpMethods.Contains(method)).ToArray();   
            }

            var validActions = method == HttpMethod.Get ? FindValidGetActions(foundActions, queryStrings) : FindValidPostActions(foundActions, queryStrings);

            if (validActions == null || !validActions.Any())
            {
                throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot find a matching action."));
            }

            if (validActions.Count() > 1)
            {
                throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.Ambiguous, "Multiple matches found."));
            }

            return validActions.FirstOrDefault();
        }

        private static IEnumerable<ReflectedHttpActionDescriptor> FindValidGetActions(
            IEnumerable<ReflectedHttpActionDescriptor> foundActions, 
            NameValueCollection queryStrings)
        {
            if (!queryStrings.HasKeys() || string.IsNullOrEmpty(queryStrings["help"]))
            {
                return foundActions;
            }

            var validActions = new List<ReflectedHttpActionDescriptor>();

            foreach (var action in foundActions)
            {
                var attribute = (QueryStringAttribute)action
                                .MethodInfo
                                .GetCustomAttributes(typeof(QueryStringAttribute), false)
                                .FirstOrDefault();
                if (attribute == null)
                {
                    validActions.Add(action);
                }
                else if (attribute.Help == Convert.ToInt32(queryStrings["help"].ToString()))
                {
                    validActions.Add(action);
                }
            }

            return validActions;
        }

        private static IEnumerable<ReflectedHttpActionDescriptor> FindValidPostActions(
            IEnumerable<ReflectedHttpActionDescriptor> foundActions, 
            NameValueCollection queryStrings)
        {
            if (queryStrings.HasKeys() && !string.IsNullOrEmpty(queryStrings["help"]))
            {
                return FindValidGetActions(foundActions, queryStrings);
            }

            var validActions = new List<ReflectedHttpActionDescriptor>();
            var submitData = HttpContext.Current.Request.Form;

            foreach (var action in foundActions)
            {
                var parameter = action.MethodInfo
                                    .GetParameters()
                                    .FirstOrDefault();
                var properties = parameter.ParameterType
                                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                .Where(p => IsPlainProperty(p));

                if (IsMatchAction(properties, submitData))
                {
                    validActions.Add(action);
                }
            }

            return validActions;
        }

        private static bool IsValidActionMethod(MethodInfo methodInfo)
        {
            if (methodInfo.IsSpecialName)
            {
                return false;
            }

            return !methodInfo.GetBaseDefinition().DeclaringType.IsAssignableFrom(typeof(ApiController));
        }

        private static bool IsPlainProperty(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(ParameterAttribute), false).Count() <= 0;
        }

        private static bool IsMatchAction(IEnumerable<PropertyInfo> properties, NameValueCollection submitData)
        {
            foreach (var property in properties)
            {
                var attribute = (ParameterAttribute)property.GetCustomAttributes(typeof(ParameterAttribute), false).FirstOrDefault();
                if (attribute != null && attribute.IsOption)
                {
                    continue;
                }

                if (submitData.AllKeys.Where(k => k.Equals(property.Name, StringComparison.CurrentCultureIgnoreCase)).Count() <= 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}