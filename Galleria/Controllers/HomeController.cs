using AutoMapper;
using Galleria.Core.Models;
using Galleria.RavenDb.BaseControllers;
using Galleria.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Galleria.Controllers
{
    public class HomeController : RavenBaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var uploadInfo = RavenSession.Query<StoredImage>().Take(1000).ToArray();
            IList<ProcessedImageViewModel> existingMedia = new List<ProcessedImageViewModel>();
            foreach (var upload in uploadInfo)
            {
                existingMedia.Add(Mapper.Map<ProcessedImageViewModel>(upload));
            }

            return View(existingMedia);
        }

        public ActionResult Upload()
        {
            return View();
        }
    }
}
