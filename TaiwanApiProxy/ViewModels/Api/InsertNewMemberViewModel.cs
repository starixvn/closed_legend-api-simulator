namespace TaiwanApiProxy.ViewModels
{
    using System.Runtime.Serialization;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.Resources;

    [DataContract]
    public class InsertNewMemberViewModel : ApiViewModel
    {
        [DataMember]
        public string SiteName { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int IBCCustId { get; set; }

        [DataMember]
        public string IBCUsername { get; set; }

        [DataMember]
        public string Firstname { get; set; }

        [DataMember]
        public string Lastname { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Otherphone { get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public byte Currency { get; set; }

        [DataMember]
        public int LASAgentID { get; set; }

        [DataMember]
        public bool IsClosed { get; set; }

        [Parameter(ParamType = ParamType.None)]
        public override string ActionName
        {
            get
            {
                return "InsertNewMember";
            }
        }

        [Parameter(ParamType = ParamType.None)]
        public override string Note
        {
            get
            {
                return Sample.InsertNewMember;
            }
        }
    }
}