using Galleria.Core.Services.FileStorage;
using System;

namespace Galleria.Core.ImageProcessing
{
    public class ExtractedImageInformation
    {
        public SavedFile File { get; set; }
        public SavedFile Preview { get; set; }
        public string Title { get; set; } 
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int Orientation { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
