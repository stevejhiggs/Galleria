using Galleria.RavenDb.BaseControllers;
using System.Web.Mvc;

namespace Galleria.Controllers
{
    public class AddController : RavenBaseController
    {
        //
        // GET: /Add/

        public ActionResult Index()
        {
            return View();
        }

    }
}
