namespace TaiwanApiProxy.Services
{
    using System.Collections.Generic;
    using TaiwanApiProxy.ViewResults;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IRuleService
    {
        IEnumerable<T> GetRules<T>();

        T GetRule<T>(T model) where T : class;

        T GetRule<T>(int ruleId) where T : class;

        void Insert<T>(T model);

        void Delete<T>(T model);

        void ChangeStatus<T>(T model);

        void Edit<T>(T model);

        void Sort<T>(T model);
    }
}
