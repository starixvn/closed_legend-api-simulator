namespace TaiwanApiProxy.ViewModels
{
    using System.Runtime.Serialization;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.Resources;

    [DataContract]
    public class UpdateMemberStatusViewModel : ApiViewModel
    {
        [DataMember]
        public string SiteName { get; set; }

        [DataMember]
        [Parameter(ParamType = ParamType.Input, IsOption = true)]
        public string IBCCustId { get; set; }

        [DataMember]
        [Parameter(ParamType = ParamType.Input, IsOption = true)]
        public string LASAgentId { get; set; }

        [DataMember]
        public bool IsClosed { get; set; }

        [Parameter(ParamType = ParamType.None)]
        public override string ActionName
        {
            get
            {
                return "UpdateMemberStatus";
            }
        }

        [Parameter(ParamType = ParamType.None)]
        public override string Note
        {
            get
            {
                return Sample.UpdateMemberStatus;
            }
        }
    }
}