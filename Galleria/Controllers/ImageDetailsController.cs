using AutoMapper;
using Galleria.Models;
using Galleria.RavenDb.Controllers;
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
            var storedImage = RavenSession.Query<StoredImage>().Where(a => a.Id == id);
            return View("Partials/EditForm", storedImage);
        }

    }
}
