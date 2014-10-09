namespace TaiwanApiProxy.Utilities
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Configurations;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.ViewModels;

    public static class ServiceWrapper
    {
        public static string GetResponse(ApiViewModel model)
        {
            var webRequest = CreateActionRequest(model);
            using (var response = webRequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var errorMessage = string.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription);
                    throw new InvalidOperationException(errorMessage);
                }

                using (StreamReader readStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return readStream.ReadToEnd();
                }
            }
        }

        private static WebRequest CreateActionRequest(ApiViewModel model)
        {
            var actionUrl = new Uri(string.Format("{0}/{1}", AppSettings.TaiwanAPIUrl, model.ActionName));
            var webRequest = WebRequest.Create(actionUrl);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            var paramData = new StringBuilder();
            var properties = model.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(model, null);
                var attribute = property.GetCustomAttributes(typeof(ParameterAttribute), true).FirstOrDefault();

                if (!IsParamRequest(attribute))
                {
                    continue;
                }

                if (propertyValue != null)
                {
                    var param = string.Format("{0}={1}&", property.Name, HttpUtility.UrlEncode(propertyValue.ToString()));
                    paramData.Append(param);
                }
            }

            byte[] formData = Encoding.UTF8.GetBytes(paramData.ToString(0, paramData.Length - 1));
            webRequest.ContentLength = formData.Length;
            using (var requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
            }

            return webRequest;
        }

        private static bool IsParamRequest(object attribute)
        {
            if (attribute == null || ((ParameterAttribute)attribute).ParamType == ParamType.Input)
            {
                return true;
            }

            return false;
        }
    }
}