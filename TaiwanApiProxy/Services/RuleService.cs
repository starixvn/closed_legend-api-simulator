namespace TaiwanApiProxy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TaiwanApiProxy.Enumerations;
    using TaiwanApiProxy.Utilities;
    using TaiwanApiProxy.ViewModels;
    using TaiwanApiProxy.ViewResults;

    public class RuleService : IRuleService
    {
        private readonly DataService _dataService;

        private readonly RuleExtension _ruleExtension;

        public RuleService()
        {
            _dataService = new DataService("~/App_Data/Rules/{0}.xml");
            _ruleExtension = new RuleExtension();
        }

        public IEnumerable<T> GetRules<T>()
        {
            return _dataService.Read<T>() ?? new List<T>();
        }

        public T GetRule<T>(T model) where T : class
        {
            var rules = _dataService.Read<T>();
            
            if (rules == null)
            {
                return null;
            }

            return rules.Where(r => _ruleExtension.CheckValidRule<T>(r, model) && _ruleExtension.IsRuleEnable<T>(r))
                        .FirstOrDefault();
        }

        public T GetRule<T>(int ruleId) where T : class
        {
            var rules = _dataService.Read<T>();

            if (rules == null)
            {
                return null;
            }

            return rules.Where(r => _ruleExtension.GetRuleId<T>(r) == ruleId)
                        .FirstOrDefault();
        }

        public void Insert<T>(T model)
        {
            var rules = _dataService.Read<T>();

            if (rules == null)
            {
                rules = new List<T>();
            }

            _ruleExtension.UpdateRuleId<T>(rules, model);

            rules.Add(model);
            _dataService.Write<T>(rules);
        }

        public void Delete<T>(T model)
        {
            var rules = _dataService.Read<T>();
            var ruleId = _ruleExtension.GetRuleId<T>(model);

            var deletedRule = rules.Where(r => _ruleExtension.GetRuleId<T>(r) == ruleId).FirstOrDefault();

            rules.Remove(deletedRule);

            _dataService.Write<T>(rules);
        }

        public void ChangeStatus<T>(T model)
        {
            var rules = _dataService.Read<T>();
            var number = rules.Count;
            var ruleId = _ruleExtension.GetRuleId<T>(model);

            for (int index = 0; index < number; index++)
            {
                if (_ruleExtension.GetRuleId<T>(rules[index]) == ruleId)
                {
                    rules[index] = model;
                    break;
                }
            }

            _dataService.Write<T>(rules.ToList());
        }

        public void Edit<T>(T model)
        {
            var rules = _dataService.Read<T>();
            var number = rules.Count;
            for (int index = 0; index < number; index++)
            {
                if (_ruleExtension.GetRuleId<T>(rules[index]) == _ruleExtension.GetRuleId<T>(model))
                {
                    _ruleExtension.UpdateRuleInfomation<T>(rules[index], model);
                    break;
                }
            }

            _dataService.Write<T>(rules.ToList());
        }

        public void Sort<T>(T model)
        {
            var sortIds = _ruleExtension.GetSortIds(model);
            var rules = _dataService.Read<T>();
            var sortRules = new List<T>();

            foreach (var sortId in sortIds)
            {
                sortRules.Add(rules.Where(r => _ruleExtension.IsMatchRule<T>(r, sortId)).FirstOrDefault());
            }

            _dataService.Write<T>(sortRules);
        }
    }
}