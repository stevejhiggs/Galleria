using AutoMapper;
using Galleria.Core.Models;
using Galleria.RavenDb.BaseControllers;
using Galleria.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
