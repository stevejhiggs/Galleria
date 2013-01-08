
namespace Galleria.ImageProcessing
{
    public class InformationExtractor
    {
        public string ImagePathBase { get; set;}
        public string ThumbnailPathBase { get; set; }
        public int ThumbnailMaxHeight { get; set; }

        public ExtractedImageInformation GetImageInformation(string filename)
        {
            ExtractedImageInformation info = new ExtractedImageInformation();
            info.FileName = filename;
            info = new TaglibExtractor().ExtractTags(ImagePathBase + filename, info);
            //probably want to do initial rotation here

            ThumbnailGenerator thumbGen = new ThumbnailGenerator();
            thumbGen.GenerateThumbnail(ImagePathBase + filename, ThumbnailPathBase + filename, ThumbnailMaxHeight);
            info.ThumbnailPath = ThumbnailPathBase + filename;
            return info;
        }
    }
}
