using Galleria.RavenDb.BaseControllers;
using System.Web.Mvc;

namespace Galleria.Controllers
{
    public class HomeController : RavenBaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }
    }
}
