using AutoMapper;
using Galleria.Core.Models;
using Galleria.RavenDb.BaseControllers;
using Galleria.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
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
            var viewModel =  Mapper.Map<EditImageDetailsViewModel>(storedImage);
            //todo handle this in the mapping
            if (storedImage.Tags != null && storedImage.Tags.Count > 0)
            {
                viewModel.ExistingTagsJson = JsonConvert.SerializeObject(storedImage.Tags.ToArray());
            }
            
            return View("Partials/EditForm", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditImageDetailsViewModel image)
        {
            if (ModelState.IsValid)
            {
                var storedImage = RavenSession.Load<StoredImage>(image.Id);
                storedImage.Name = image.Name;

                //break apart the tags
                if (image.HiddenTags != null)
                {
                    string[] tags = image.HiddenTags.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                    storedImage.Tags = new List<string>(tags);
                }
                else
                {
                    storedImage.Tags = null;
                }

                RavenSession.Store(storedImage);

                return Json(Mapper.Map<ProcessedImageViewModel>(storedImage));
            }

            return View("Partials/EditForm", image);
        }

    }
}
