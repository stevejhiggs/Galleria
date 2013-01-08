using AutoMapper;
using Galleria.Models;
using Galleria.RavenDb.Controllers;
using Galleria.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            var uploadInfo = RavenSession.Query<StoredImage>().Take(500).ToArray();
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
