using AutoMapper;
using Galleria.Core.ImageProcessing;
using Galleria.Core.Models;
using Galleria.Core.Services.FileStorage;
using Galleria.Hubs;
using Galleria.RavenDb.BaseControllers;
using Galleria.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;

namespace Galleria.Controllers
{
    public class FileAssemblyController : RavenBaseApiController
    {
        IChunkedFileStorageService ChunkedFileStorageService;

        private string mediapath;
        private string thumbnailPath;

        public FileAssemblyController(IChunkedFileStorageService chunkedFileStorageService)
        {
            ChunkedFileStorageService = chunkedFileStorageService;
            mediapath = ConfigurationManager.AppSettings["MediaPath"];
            thumbnailPath = ConfigurationManager.AppSettings["PreviewPath"];
        }

        // GET api/fileassembly
        public async Task<ISavedFile> Post(FileAssemblyRequest item)
        {
            ISavedFile savedFileInformation = await ChunkedFileStorageService.AssembleFileFromBlocksAsync(item);

            string mappedFilePath = HttpContext.Current.Server.MapPath(mediapath);
            string mappedThumbnailPath = HttpContext.Current.Server.MapPath(thumbnailPath);

            //process the image
            //TODO: Thumbnail writing should be handled via the file service
            InformationExtractor extractor = new InformationExtractor();
            extractor.ImagePathBase = mappedFilePath;
            extractor.ThumbnailPathBase = mappedThumbnailPath;
            extractor.ThumbnailMaxHeight = 200;
            ExtractedImageInformation exInfo = extractor.GetImageInformation(savedFileInformation);

            //write to raven
            StoredImage info = Mapper.Map<StoredImage>(exInfo);
            info.UploadDateTime = DateTime.Now;
            await RavenSession.StoreAsync(info);

            //signal the hub that we are done
            var context = GlobalHost.ConnectionManager.GetHubContext<PictureProcessHub>();
            context.Clients.All.pictureprocessed(Mapper.Map<ProcessedImageViewModel>(info));

            return savedFileInformation;
        }
    }

    
}
