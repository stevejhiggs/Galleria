﻿using AutoMapper;
using Galleria.Hubs;
using Galleria.ImageProcessing;
using Galleria.Models;
using Galleria.RavenDb.Controllers;
using Galleria.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;

namespace Galleria.Controllers
{
    public class FileAssemblyController : RavenBaseApiController
    {
        private string blockpath;
        private string mediapath;
        private string thumbnailPath;

        public FileAssemblyController()
        {
            blockpath = ConfigurationManager.AppSettings["BlockPath"];
            mediapath = ConfigurationManager.AppSettings["MediaPath"];
            thumbnailPath = ConfigurationManager.AppSettings["ThumbnailPath"];
        }

        // GET api/fileassembly
        public void Post(AssembleRequest item)
        {
            string mappedBlockPath = HttpContext.Current.Server.MapPath(blockpath);
            string mappedFilePath = HttpContext.Current.Server.MapPath(mediapath);
            string mappedThumbnailPath = HttpContext.Current.Server.MapPath(thumbnailPath);
            string destinationFile = mappedFilePath + item.filename;

            List<string> sourcefiles = new List<string>();

            //build up the blockstrings
            foreach(string block in item.blocks)
            {
                sourcefiles.Add(mappedBlockPath + block);
            }

            using (Stream destStream = File.OpenWrite(destinationFile))
            {
                foreach (string srcFileName in sourcefiles)
                {
                    using (Stream srcStream = File.OpenRead(srcFileName))
                    {
                        srcStream.CopyTo(destStream);
                    }
                }
            }

            //tidy up the old blocks
            foreach (string srcFileName in sourcefiles)
            {
                File.Delete(srcFileName);
            }

            //following should just be added to a processing queue

            //write to raven
            UploadInformation info = new UploadInformation{ Id = Guid.NewGuid().ToString(), Filename = item.filename, Name = item.filename};
            RavenSession.Store(info);

            //process the image
            ThumbnailGenerator generator = new ThumbnailGenerator();
            generator.GenerateThumbnail(mappedFilePath + item.filename, mappedThumbnailPath + item.filename);

            //signal the hub that we are done
            var context = GlobalHost.ConnectionManager.GetHubContext<PictureProcessHub>();
            context.Clients.All.pictureprocessed(Mapper.Map<ProcessedImageViewModel>(info));
        }
    }

    public class AssembleRequest
    {
        public string filename { get; set; }
        public string[] blocks { get; set; }
    }
}
