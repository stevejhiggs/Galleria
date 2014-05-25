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
using System.Web.Http;

namespace Galleria.Controllers.Api
{
	[RoutePrefix("api/files")]
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

		[HttpPost]
		[Route("assemble")]
        public async Task<SavedFile> Assemble(FileAssemblyRequest item)
        {
            SavedFile savedFileInformation = await ChunkedFileStorageService.AssembleFileFromBlocksAsync(item);

			//todo, get file information back from save call
			byte[] fileContents = ChunkedFileStorageService.RetrieveFileContents(savedFileInformation);

            string mappedFilePath = HttpContext.Current.Server.MapPath(mediapath);
            string mappedThumbnailPath = HttpContext.Current.Server.MapPath(thumbnailPath);

			ExtractedImageInformation exInfo = new ExtractedImageInformation();
			exInfo.File = savedFileInformation;

            InformationExtractor extractor = new InformationExtractor();
            extractor.ImagePathBase = mappedFilePath;
			extractor.GetImageInformation(savedFileInformation, fileContents, savedFileInformation.StorageFileName, ref exInfo);

			//generate thumbnail
			byte[] thumbnailBytes = new ThumbnailGenerator().GenerateThumbnail(fileContents);
			exInfo.Preview = ChunkedFileStorageService.SaveFileWithoutChunking(savedFileInformation.UploadedFileName, "thumbnail", thumbnailBytes);

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
