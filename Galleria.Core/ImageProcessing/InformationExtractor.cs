using Galleria.Core.Services.FileStorage;
using System.IO;

namespace Galleria.Core.ImageProcessing
{
    public class InformationExtractor
    {
        public string ImagePathBase { get; set;}
        public string ThumbnailPathBase { get; set; }
        public int ThumbnailMaxHeight { get; set; }

        public ExtractedImageInformation GetImageInformation(ISavedFile savedFileInformation)
        {
            //TODO, I Should be a parallel async call
            ExtractedImageInformation info = new ExtractedImageInformation();
            info.FileName = savedFileInformation.Filename;
            info = new TaglibExtractor().ExtractTags(ImagePathBase + info.FileName, info);
            if (string.IsNullOrWhiteSpace(info.Name))
            {
                info.Name = savedFileInformation.Name;
            }

            //probably want to do initial rotation here

            //for now read out the file directly
            byte[] input = File.ReadAllBytes(ImagePathBase + info.FileName);

            ThumbnailGenerator thumbGen = new ThumbnailGenerator();
            byte[] thumbnailBytes = thumbGen.GenerateThumbnail(input, ThumbnailMaxHeight);

            //for now save file directly
            File.WriteAllBytes(ThumbnailPathBase + info.FileName, thumbnailBytes);
            info.ThumbnailPath = ThumbnailPathBase + info.FileName;
            return info;
        }
    }
}
