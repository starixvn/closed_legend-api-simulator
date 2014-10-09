namespace TaiwanApiProxy.Services
{
    using System.Collections.Generic;
    using TaiwanApiProxy.Entities;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ILogService
    {
        IEnumerable<T> GetLogs<T>();

        void Insert<T>(T log);

        void Delete<T>(T log);
    }
}
