using AutoMapper;
using Galleria.Core.Models;
using Galleria.RavenDb.BaseControllers;
using Galleria.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Galleria.Controllers
{
    public class MapController : RavenBaseController
    {
        //
        // GET: /Map/

        public ActionResult Index()
        {
            var uploadInfo = RavenSession.Query<StoredImage>().OrderByDescending(i => i.UploadDateTime).Take(1000).ToArray();
            IList<ProcessedImageViewModel> existingMedia = new List<ProcessedImageViewModel>();
            foreach (var upload in uploadInfo)
            {
                if (upload.Latitude.HasValue && upload.Longitude.HasValue)
                {
                    existingMedia.Add(Mapper.Map<ProcessedImageViewModel>(upload));
                }
            }

            var model = new MapViewModel();
            model.Images = existingMedia;
            if (existingMedia.Count > 0)
            {
                model.CentreLatitude = existingMedia[0].Latitude.Value;
                model.CentreLongitude = existingMedia[0].Longitude.Value;
            }

            model.ZoomLevel = 10;

            return View(model);
        }
    }
}
