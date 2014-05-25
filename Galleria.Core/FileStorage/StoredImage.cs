using System;
using System.Collections.Generic;

namespace Galleria.Core.FileStorage
{
    public class StoredImage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public SavedFile File { get; set; }
        public SavedFile Preview { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int Orientation { get; set; }
        public DateTime? Timestamp { get; set; }
        public List<string> Tags { get; set; }
        public DateTime UploadDateTime { get; set; }
    }
}