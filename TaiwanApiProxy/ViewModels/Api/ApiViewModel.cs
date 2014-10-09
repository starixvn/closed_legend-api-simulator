namespace TaiwanApiProxy.ViewModels
{
    using System;
    using System.Runtime.Serialization;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Enumerations;

    [DataContract]
    public abstract class ApiViewModel
    {
        public ApiViewModel()
        {
            RuleStatus = true;
        }

        [Parameter(ParamType = ParamType.None)]
        public int Id { get; set; }

        [Parameter(ParamType = ParamType.None)]
        public string Sort { get; set; }

        [Parameter(ParamType = ParamType.Output)]
        public string Response { get; set; }

        [Parameter(ParamType = ParamType.Status)]
        public bool RuleStatus { get; set; }

        public abstract string ActionName { get; }

        public abstract string Note { get; }
    }
}