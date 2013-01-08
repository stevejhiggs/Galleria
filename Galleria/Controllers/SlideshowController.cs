using Galleria.Models;
using Galleria.RavenDb.Controllers;
using Galleria.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;

namespace Galleria.Controllers
{
    public class SlideshowController : RavenBaseController
    {
        //
        // GET: /Slideshow/

        public ActionResult Index()
        {
            var uploadInfo = RavenSession.Query<UploadInformation>().Take(500).ToArray();
            IList<ProcessedImageViewModel> existingMedia = new List<ProcessedImageViewModel>();
            foreach (var upload in uploadInfo)
            {
                existingMedia.Add(Mapper.Map<ProcessedImageViewModel>(upload));
            }

            return View(existingMedia);
        }
    }
}
