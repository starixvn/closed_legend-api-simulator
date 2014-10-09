namespace TaiwanApiProxy.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TaiwanApiProxy.Attributes;
    using TaiwanApiProxy.Entities;
    using TaiwanApiProxy.Enumerations;

    public class RuleExtension
    {
        public bool CheckValidRule<T>(T rule, T model)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var property in properties)
            {
                var attribute = (ParameterAttribute)property.GetCustomAttributes(typeof(ParameterAttribute), false).FirstOrDefault();
                var ruleValue = Convert.ChangeType(property.GetValue(rule, null), property.PropertyType);
                var modelValue = Convert.ChangeType(property.GetValue(model, null), property.PropertyType);

                if ((attribute != null && attribute.ParamType == ParamType.None) 
                    || ruleValue == null
                    || (modelValue != null
                    && (property.PropertyType.Equals(typeof(string))
                    && ruleValue.ToString().Equals(modelValue.ToString(), StringComparison.CurrentCultureIgnoreCase))))
                {
                    continue;
                }

                if (!ruleValue.Equals(modelValue))
                {
                    return false;
                }
            }

            return true;
        }

        public void UpdateRuleId<T>(IList<T> rules, T model)
        {
            if (rules.Count == 0)
            {
                SetRuleId<T>(model, 1);
            }

            int maxRule = GetMaxRuleId<T>(rules);

            SetRuleId<T>(model, maxRule + 1);
        }

        public void UpdateRuleInfomation<T>(T rule, T model)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            foreach (var property in properties)
            {
                var attribute = (ParameterAttribute)property.GetCustomAttributes(typeof(ParameterAttribute), false).FirstOrDefault();

                if (attribute == null)
                {
                    property.SetValue(rule, property.GetValue(model, null), null);
                    continue;
                }

                if (attribute != null && (attribute.ParamType == ParamType.Input || attribute.ParamType == ParamType.Output))
                {
                    property.SetValue(rule, property.GetValue(model, null), null);
                    continue;
                }
            }
        }

        public IEnumerable<int> GetSortIds<T>(T model)
        {
            var sortIds = typeof(T).GetProperty("Sort")
                                .GetValue(model, null)
                                .ToString()
                                .Split(',');

            foreach (var sortId in sortIds)
            {
                yield return Convert.ToInt32(sortId);
            }
        }

        public bool IsMatchRule<T>(T rule, int id)
        {
            return GetRuleId<T>(rule) == id;
        }

        public int GetRuleId<T>(T rule)
        {
            return Convert.ToInt32(typeof(T).GetProperty("Id").GetValue(rule, null));
        }

        public void SetRuleId<T>(T rule, int id)
        {
            typeof(T).GetProperty("Id").SetValue(rule, id, null);
        }

        public bool IsRuleEnable<T>(T rule)
        {
            return Convert.ToBoolean(typeof(T).GetProperty("RuleStatus").GetValue(rule, null));
        }

        public string GetActionName<T>(T rule)
        {
            return Convert.ToString(typeof(T).GetProperty("ActionName").GetValue(rule, null));
        }

        public string GetNote<T>(T rule)
        {
            return Convert.ToString(typeof(T).GetProperty("Note").GetValue(rule, null));
        }

        public IEnumerable<Parameter> GetParametersValue<T>(T rule)
        {
            var properties = rule.GetType().GetProperties();

            foreach (var property in properties)
            {
                var attribute = (ParameterAttribute)property.GetCustomAttributes(typeof(ParameterAttribute), false).FirstOrDefault();

                if (attribute == null || (attribute.ParamType != ParamType.None && attribute.ParamType != ParamType.Status))
                {
                    yield return new Parameter
                    {
                        Name = property.Name,
                        Type = property.PropertyType.Name,
                        IsOutput = attribute != null && attribute.ParamType == ParamType.Output,
                        Value = Convert.ChangeType(property.GetValue(rule, null), property.PropertyType)
                    };
                }
            }
        }

        private int GetMaxRuleId<T>(IList<T> rules)
        {
            int maxId = 0;

            foreach (var rule in rules)
            {
                var id = Convert.ToInt32(typeof(T).GetProperty("Id").GetValue(rule, null));
                if (id > maxId)
                {
                    maxId = id;
                }
            }

            return maxId;
        }
    }
}