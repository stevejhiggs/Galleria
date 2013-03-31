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
            var uploadInfo = RavenSession.Query<StoredImage>().OrderByDescending(i => i.UploadDateTime).Take(1000).ToArray();
            IList<ProcessedImageViewModel> existingMedia = new List<ProcessedImageViewModel>();
            foreach (var upload in uploadInfo)
            {
                var item = Mapper.Map<ProcessedImageViewModel>(upload);
                item.LazyLoadPlaceholderUrl = Url.Content("~/Content/Images/ImagePlaceholder.png");
                existingMedia.Add(item);
            }

            var viewModel = new IndexViewModel();
            viewModel.ImageCollectionJson = new HtmlString(JsonConvert.SerializeObject(existingMedia.ToArray()));

            return View(viewModel);
        }

        public ActionResult Upload()
        {
            return View();
        }
    }
}
