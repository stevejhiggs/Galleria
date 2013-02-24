using Galleria.Services.FileStorage;
using System;

namespace Galleria.Models
{
    public class StoredImage : ISavedFile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int Orientation { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}