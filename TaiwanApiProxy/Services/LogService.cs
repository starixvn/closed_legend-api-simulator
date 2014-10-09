namespace TaiwanApiProxy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TaiwanApiProxy.Configurations;
    using TaiwanApiProxy.Entities;
    using TaiwanApiProxy.Utilities;

    public class LogService : ILogService
    {
        private readonly DataService _dataService;

        private readonly RuleExtension _ruleExtension;

        public LogService()
        {
            _dataService = new DataService("~/App_Data/Logs/{0}.xml");
            _ruleExtension = new RuleExtension();
        }

        public void Insert<T>(T log)
        {
            var logs = _dataService.Read<T>();

            if (logs == null)
            {
                logs = new List<T>();
            }
            else if (logs.Count >= AppSettings.NumberLog)
            {
                logs = logs.Skip<T>(1).ToList();
            }

            _ruleExtension.UpdateRuleId<T>(logs, log);

            logs.Add(log);

            _dataService.Write<T>(logs);
        }

        public IEnumerable<T> GetLogs<T>()
        {
            return _dataService.Read<T>();
        }

        public void Delete<T>(T log)
        {
            var rules = _dataService.Read<T>();

            var deletedRule = rules.Where(r => _ruleExtension.CheckValidRule<T>(r, log)).FirstOrDefault();

            rules.Remove(deletedRule);

            _dataService.Write<T>(rules);
        }
    }
}