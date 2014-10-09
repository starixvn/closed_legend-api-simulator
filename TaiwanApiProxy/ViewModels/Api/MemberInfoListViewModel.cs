namespace TaiwanApiProxy.ViewModels
{
    using System.Runtime.Serialization;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.Resources;

    [DataContract]
    public class MemberInfoListViewModel : ApiViewModel
    {
        [DataMember]
        public string SiteName { get; set; }

        [DataMember]
        public int LASAgentID { get; set; }

        [Parameter(ParamType = ParamType.None)]
        public override string ActionName
        {
            get
            {
                return "GetMemberInfoList";
            }
        }

        [Parameter(ParamType = ParamType.None)]
        public override string Note
        {
            get
            {
                return Sample.GetMemberInfoList_0;
            }
        }
    }
}