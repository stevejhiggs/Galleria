using ImageResizer;
using System.IO;

namespace Galleria.Core.ImageProcessing
{
    public class ThumbnailGenerator
    {
		public int MaxHeight = 200;

        public byte[] GenerateThumbnail(byte[] sourceArray)
        {
            using (var outStream = new MemoryStream())
            {
                using (var inStream = new MemoryStream(sourceArray))
                {
					var settings = new ResizeSettings("height=" + MaxHeight + "&mode=pad&bgcolor=FFFFFF&anchor=middlecenter&scale=upscalecanvas");
                    ImageBuilder.Current.Build(inStream, outStream, settings);
                    return outStream.ToArray();
                }
            }
        }
    }
}
