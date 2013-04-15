using Galleria.Core.Services.FileStorage;
using System.IO;

namespace Galleria.Core.ImageProcessing
{
    public class InformationExtractor
    {
        public string ImagePathBase { get; set;}
        public string ThumbnailPathBase { get; set; }
        public int ThumbnailMaxHeight { get; set; }

        public ExtractedImageInformation GetImageInformation(ISavedFile savedFileInformation, IFileStorageService fileStorageService)
        {
            byte[] input = fileStorageService.RetrieveFileContents(savedFileInformation, FileType.File);

            //TODO, I Should be a parallel async call
            ExtractedImageInformation info = new ExtractedImageInformation();
            info.File = new SavedFile();
            info.Preview = new SavedFile();
            info.File.UploadedFileName = info.Preview.UploadedFileName = savedFileInformation.UploadedFileName;

            info.File.StorageFileName = savedFileInformation.StorageFileName;
            info = new TaglibExtractor().ExtractTags(fileStorageService.GetFileStorageUri(savedFileInformation.StorageFileName, FileType.File), input, info);
            if (string.IsNullOrWhiteSpace(info.Title))
            {
                info.Title = savedFileInformation.UploadedFileName;
            }

            //probably want to do initial rotation here

            ThumbnailGenerator thumbGen = new ThumbnailGenerator();
            byte[] thumbnailBytes = thumbGen.GenerateThumbnail(input, ThumbnailMaxHeight);
            ISavedFile thumbnailFile = fileStorageService.SaveFileWithoutChunking(savedFileInformation.UploadedFileName, FileType.Preview, thumbnailBytes);

            info.Preview.StorageFileName = thumbnailFile.StorageFileName;
            return info;
        }
    }
}
