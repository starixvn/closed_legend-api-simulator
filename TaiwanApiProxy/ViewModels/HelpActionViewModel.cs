namespace TaiwanApiProxy.ViewModels
{
    using System.Collections.Generic;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Entities;

    public class HelpActionViewModel
    {
        public string Action { get; set; }

        public List<Parameter> Parameters { get; set; }

        public int Help { get; set; }

        public string Note { get; set; }
    }
}