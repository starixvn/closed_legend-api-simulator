namespace TaiwanApiProxy.Attributes
{
    using System;
    using TaiwanApiProxy.Enumerations;

    public class ParameterAttribute : Attribute
    {
        public ParamType ParamType { get; set; }

        public bool IsOption { get; set; }
    }
}