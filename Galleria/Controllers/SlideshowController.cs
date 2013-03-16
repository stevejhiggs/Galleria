using AutoMapper;
using Galleria.Core.Models;
using Galleria.RavenDb.BaseControllers;
using Galleria.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Galleria.Controllers
{
    public class SlideshowController : RavenBaseController
    {
        //
        // GET: /Slideshow/

        public ActionResult Index()
        {
            var uploadInfo = RavenSession.Query<StoredImage>().Take(500).ToArray();
            IList<ProcessedImageViewModel> existingMedia = new List<ProcessedImageViewModel>();
            foreach (var upload in uploadInfo)
            {
                existingMedia.Add(Mapper.Map<ProcessedImageViewModel>(upload));
            }

            return View(existingMedia);
        }
    }
}
