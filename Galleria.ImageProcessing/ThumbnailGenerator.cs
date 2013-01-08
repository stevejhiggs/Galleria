using ImageResizer;

namespace Galleria.ImageProcessing
{
    public class ThumbnailGenerator
    {
        public void GenerateThumbnail(string sourceUrl, string destinationUrl)
        {
            ImageBuilder.Current.Build(sourceUrl, destinationUrl, new ResizeSettings("height=" + 200 + "&mode=pad&bgcolor=FFFFFF&anchor=middlecenter&scale=upscalecanvas"));
        }
    }
}
