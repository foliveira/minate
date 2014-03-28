namespace Minate.Controllers
{
    using System.Web.Mvc;

    [HandleError]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Terms()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}
