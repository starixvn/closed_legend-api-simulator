namespace TaiwanApiProxy.ViewModels
{
    using System.Runtime.Serialization;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.Resources;

    [DataContract]
    public class MemberInfoViewModel : ApiViewModel
    {
        [DataMember]
        public string SiteName { get; set; }

        [DataMember]
        public int IBCCustId { get; set; }

        [Parameter(ParamType = ParamType.None)]
        public override string ActionName
        {
            get
            {
                return "GetMemberInfo";
            }
        }

        [Parameter(ParamType = ParamType.None)]
        public override string Note
        {
            get
            {
                return Sample.GetMemberInfo;
            }
        }
    }
}