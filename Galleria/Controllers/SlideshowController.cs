using AutoMapper;
using Galleria.Core.FileStorage;
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

        public ActionResult Index(string startImageId)
        {
            var uploadInfo = RavenSession.Query<StoredImage>().Take(500).ToArray();
            IList<ProcessedImageViewModel> existingMedia = new List<ProcessedImageViewModel>();
            int activeSlideNumber = 1;
            int imageCounter = 0;
            foreach (var upload in uploadInfo)
            {
                existingMedia.Add(Mapper.Map<ProcessedImageViewModel>(upload));

                if (startImageId == upload.Id)
                {
                    activeSlideNumber = imageCounter + 1;
                }

                imageCounter++;
            }

            var viewModel = new SlideShowViewModel();
            viewModel.Images = existingMedia;
            viewModel.StartNumber = activeSlideNumber;

            return View(viewModel);
        }
    }
}
