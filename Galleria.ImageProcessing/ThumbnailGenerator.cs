using ImageResizer;

namespace Galleria.ImageProcessing
{
    public class ThumbnailGenerator
    {
        public void GenerateThumbnail(string sourcePath, string destinationPath, int maxHeight)
        {
            ImageBuilder.Current.Build(sourcePath, destinationPath, new ResizeSettings("height=" + maxHeight + "&mode=pad&bgcolor=FFFFFF&anchor=middlecenter&scale=upscalecanvas"));
        }
    }
}
