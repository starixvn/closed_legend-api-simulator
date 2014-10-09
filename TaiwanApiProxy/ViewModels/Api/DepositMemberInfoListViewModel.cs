namespace TaiwanApiProxy.ViewModels
{
    using System.Runtime.Serialization;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.Resources;

    [DataContract]
    public class DepositMemberInfoListViewModel : ApiViewModel
    {
        [DataMember]
        public string SiteName { get; set; }

        [DataMember]
        public int LASAgentID { get; set; }

        [DataMember]
        public int DepositOption { get; set; }

        [Parameter(ParamType = ParamType.Input, IsOption = true)]
        public string Username { get; set; }

        [Parameter(ParamType = ParamType.Input, IsOption = true)]
        public string IBCCustIdList { get; set; }

        [Parameter(ParamType = ParamType.None)]
        public override string ActionName
        {
            get
            {
                return "GetDepositMemberInfoList";
            }
        }

        [Parameter(ParamType = ParamType.None)]
        public override string Note
        {
            get
            {
                return Sample.GetDepositMemberInfoList;
            }
        }
    }
}