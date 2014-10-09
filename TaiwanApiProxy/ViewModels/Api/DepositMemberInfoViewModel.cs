namespace TaiwanApiProxy.ViewModels
{
    using System.Runtime.Serialization;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.Resources;

    [DataContract]
    public class DepositMemberInfoViewModel : ApiViewModel
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
                return "GetDepositMemberInfo";
            }
        }

        [Parameter(ParamType = ParamType.None)]
        public override string Note
        {
            get
            {
                return Sample.GetDepositMemberInfo;
            }
        }
    }
}