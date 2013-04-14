using ImageResizer;
using System.IO;

namespace Galleria.Core.ImageProcessing
{
    public class ThumbnailGenerator
    {
        public byte[] GenerateThumbnail(byte[] sourceArray, int maxHeight)
        {
            using (var outStream = new MemoryStream())
            {
                using (var inStream = new MemoryStream(sourceArray))
                {
                    var settings = new ResizeSettings("height=" + maxHeight + "&mode=pad&bgcolor=FFFFFF&anchor=middlecenter&scale=upscalecanvas");
                    ImageBuilder.Current.Build(inStream, outStream, settings);
                    return outStream.ToArray();
                }
            }
        }
    }
}
