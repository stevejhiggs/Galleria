using System;

namespace Galleria.Core.ImageProcessing
{
    public class ExtractedImageInformation
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string ThumbnailPath { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int Orientation { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
