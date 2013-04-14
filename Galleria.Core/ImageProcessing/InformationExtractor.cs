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
            info.FileName = savedFileInformation.FileName;
            info = new TaglibExtractor().ExtractTags(fileStorageService.GetFileStorageUri(savedFileInformation, FileType.File), input, info);
            if (string.IsNullOrWhiteSpace(info.Name))
            {
                info.Name = savedFileInformation.Name;
            }

            //probably want to do initial rotation here

            

            ThumbnailGenerator thumbGen = new ThumbnailGenerator();
            byte[] thumbnailBytes = thumbGen.GenerateThumbnail(input, ThumbnailMaxHeight);

            //for now save file directly
            File.WriteAllBytes(ThumbnailPathBase + info.FileName, thumbnailBytes);
            info.ThumbnailPath = ThumbnailPathBase + info.FileName;
            return info;
        }
    }
}
