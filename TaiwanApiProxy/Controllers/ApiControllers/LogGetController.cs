namespace TaiwanApiProxy.Controllers
{
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using TaiwanApiProxy.Entities;
    using TaiwanApiProxy.Services;
    using TaiwanApiProxy.Utilities;

    public class LogGetController : ApiController
    {
        private readonly ILogService _logService;

        public LogGetController()
        {
            _logService = new LogService();
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage Index()
        {
            var logs = _logService.GetLogs<LogInformation>();

            return ViewParser.Get("Log", "Index", logs.OrderByDescending(l => l.LogTime), new LogController());
        }
    }
}
