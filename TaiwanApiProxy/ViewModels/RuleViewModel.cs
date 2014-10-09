namespace TaiwanApiProxy.ViewModels
{
    using System.Collections.Generic;
    using TaiwanApiProxy.Entities;

    public class RuleViewModel
    {
        public int RuleId { get; set; }

        public string Action { get; set; }

        public List<Parameter> Parameters { get; set; }

        public int Help { get; set; }

        public string Note { get; set; }
    }
}