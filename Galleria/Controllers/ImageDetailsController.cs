using AutoMapper;
using Galleria.Models;
using Galleria.RavenDb.BaseControllers;
using Galleria.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Galleria.Controllers
{
    public class ImageDetailsController : RavenBaseController
    {
        //
        // GET: /ImageDetails/Details/5

        public ActionResult Edit(string id)
        {
            var storedImage = RavenSession.Load<StoredImage>(id);
            return View("Partials/EditForm", Mapper.Map<EditImageDetailsViewModel>(storedImage));
        }

        [HttpPost]
        public ActionResult Edit(EditImageDetailsViewModel image)
        {
            var storedImage = RavenSession.Load<StoredImage>(image.Id);
            storedImage.Name = image.Name;
            RavenSession.Store(storedImage);

            return View("Partials/EditForm", image);
        }

    }
}
