using AutoMapper;
using Galleria.Core.ImageProcessing;
using Galleria.Core.FileStorage;
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
        }

		[HttpPost]
		[Route("assemble")]
        public async Task<SavedFile> Assemble(FileAssemblyRequest item)
        {
            SavedFile savedFileInformation = await ChunkedFileStorageService.AssembleFileFromBlocksAsync(item);

			//todo, get file information back from save call
			byte[] fileContents = ChunkedFileStorageService.RetrieveFileContents(savedFileInformation);

			ExtractedImageInformation exInfo = new ExtractedImageInformation();
			exInfo.File = savedFileInformation;

            InformationExtractor extractor = new InformationExtractor();
			exInfo = extractor.GetImageInformation(savedFileInformation, fileContents, exInfo);

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
