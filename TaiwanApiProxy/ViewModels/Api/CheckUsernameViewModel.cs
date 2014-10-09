namespace TaiwanApiProxy.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.Resources;

    [DataContract]
    public class CheckUsernameViewModel : ApiViewModel
    {
        [DataMember]
        public string SiteName { get; set; }

        [DataMember]
        public string Username { get; set; }

        [Parameter(ParamType = ParamType.None)]
        public override string ActionName
        {
            get
            {
                return "CheckUserName";
            }
        }

        [Parameter(ParamType = ParamType.None)]
        public override string Note 
        {
            get
            {
                return Sample.CheckUserName;
            }
        }
    }
}