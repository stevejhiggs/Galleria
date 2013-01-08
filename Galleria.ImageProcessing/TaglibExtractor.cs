
namespace Galleria.ImageProcessing
{
    public class TaglibExtractor
    {
        public ExtractedImageInformation ExtractTags(string sourcePath, ExtractedImageInformation info)
        {
            var imageFile = TagLib.File.Create(sourcePath) as TagLib.Image.File;
            info.Orientation = (int)imageFile.ImageTag.Orientation;
            info.Latitude = imageFile.ImageTag.Latitude;
            info.Longitude = imageFile.ImageTag.Latitude;
            info.Timestamp = imageFile.ImageTag.DateTime;
            info.Name = imageFile.ImageTag.Title;
            return info;
        }
    }
}
