namespace TaiwanApiProxy.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Entities;
    using TaiwanApiProxy.Enumerations;

    public static class ActionExtension
    {
        public static IEnumerable<Parameter> GetParameters(IEnumerable<MethodInfo> methodInfos, int help)
        {
            var methodInfo = GetMethodByHelp(methodInfos, help);
            var model = methodInfo.GetParameters().FirstOrDefault();
            
            return GetParameters(model.ParameterType);
        }

        private static MethodInfo GetMethodByHelp(IEnumerable<MethodInfo> methodInfos, int help)
        {
            foreach (var methodInfo in methodInfos)
            {
                var attribute = methodInfo.GetCustomAttributes(typeof(QueryStringAttribute), false)
                                    .Where(attr => ((QueryStringAttribute)attr).Help == help)
                                    .FirstOrDefault();

                if (attribute != null)
                {
                    return methodInfo;
                }
            }

            return methodInfos.FirstOrDefault();
        }

        private static IEnumerable<Parameter> GetParameters(Type parameterType)
        {
            var properties = parameterType.GetProperties();

            foreach (var property in properties)
            {
                var attribute = (ParameterAttribute)property.GetCustomAttributes(typeof(ParameterAttribute), false).FirstOrDefault();

                if (attribute == null || attribute.ParamType == ParamType.Input)
                {
                    yield return new Parameter
                    {
                        Name = property.Name,
                        Type = property.PropertyType.Name
                    };
                }
            }
        }
    }
}