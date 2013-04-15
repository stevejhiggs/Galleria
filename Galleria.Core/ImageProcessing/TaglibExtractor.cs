using System;
using System.IO;

namespace Galleria.Core.ImageProcessing
{
    public class TaglibExtractor
    {
        public ExtractedImageInformation ExtractTags(string sourcePath, byte[] sourceArray, ExtractedImageInformation info)
        {
            using (var file = new MemoryStreamFileAbstraction(sourcePath, sourceArray))
            {
                using (var imageFile = TagLib.File.Create(file) as TagLib.Image.File)
                {
                    info.Orientation = (int)imageFile.ImageTag.Orientation;
                    info.Latitude = imageFile.ImageTag.Latitude;
                    info.Longitude = imageFile.ImageTag.Latitude;
                    info.Timestamp = imageFile.ImageTag.DateTime;
                    info.Title = imageFile.ImageTag.Title;
                    return info;
                }
            }
        }

        public class MemoryStreamFileAbstraction : TagLib.File.IFileAbstraction, IDisposable
        {
            private MemoryStream stream;
            private string name;

            public MemoryStreamFileAbstraction(string name, byte[] input)
            {
                this.stream = new MemoryStream(input);
                this.name = name;
            }

            public string Name
            {
                get { return name; }
            }

            public System.IO.Stream ReadStream
            {
                get { return stream; }
            }

            public System.IO.Stream WriteStream
            {
                get { throw new Exception("Cannot write to this abstraction"); }
            }

            public void CloseStream(System.IO.Stream stream) 
            {
                stream.Close();
            }

            public void Dispose()
            {
                stream.Dispose();
            }
        }
    }
}
