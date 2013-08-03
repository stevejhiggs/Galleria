using Galleria.Core.Services.FileStorage;
using System.IO;
using System.Text.RegularExpressions;

namespace Galleria.Core.ImageProcessing
{
    public class InformationExtractor
    {
        public string ImagePathBase { get; set;}
        public string ThumbnailPathBase { get; set; }
        public int ThumbnailMaxHeight { get; set; }

        public ExtractedImageInformation GetImageInformation(SavedFile savedFileInformation, IFileStorageService fileStorageService)
        {
            byte[] input = fileStorageService.RetrieveFileContents(savedFileInformation);

            //TODO, I Should be a parallel async call
            ExtractedImageInformation info = new ExtractedImageInformation();
            info.File = new SavedFile();
            info.Preview = new SavedFile();
            info.File.UploadedFileName = info.Preview.UploadedFileName = savedFileInformation.UploadedFileName;

            info.File.StorageFileName = savedFileInformation.StorageFileName;
            info = new TaglibExtractor().ExtractTags(fileStorageService.GetFileStorageUri(savedFileInformation), input, info);
            if (string.IsNullOrWhiteSpace(info.Title))
            {
                info.Title = MakeTitleFromFileName(savedFileInformation.UploadedFileName);
            }

            //probably want to do initial rotation here

            ThumbnailGenerator thumbGen = new ThumbnailGenerator();
            byte[] thumbnailBytes = thumbGen.GenerateThumbnail(input, ThumbnailMaxHeight);
            SavedFile thumbnailFile = fileStorageService.SaveFileWithoutChunking(savedFileInformation.UploadedFileName, "thumbnail", thumbnailBytes);

            info.Preview = thumbnailFile;
            return info;
        }

        private string MakeTitleFromFileName(string fileName)
        {
            int dotIndex = fileName.LastIndexOf('.');
            if (dotIndex > -1)
            {
                fileName = fileName.Substring(0, dotIndex);
            }

            //todo: handle international characters
            fileName = Regex.Replace(fileName, "[^A-Za-z0-9 ]", " ");

            return fileName;
        }
    }
}
