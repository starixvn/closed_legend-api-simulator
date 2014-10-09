namespace TaiwanApiProxy.ViewModels
{
    using System.Collections.Generic;

    public class RuleListViewModel
    {
        public string Action { get; set; }

        public IEnumerable<string> Header { get; set; }

        public IEnumerable<ApiViewModel> ApiViewModel { get; set; }

        public int Help { get; set; } 
    }
}