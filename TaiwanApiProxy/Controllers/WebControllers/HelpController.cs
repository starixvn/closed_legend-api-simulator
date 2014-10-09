namespace TaiwanApiProxy.Controllers
{
    using System.Web.Mvc;

    public class HelpController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
