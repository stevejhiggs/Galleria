using System;

namespace Galleria.ViewModels
{
    public class ProcessedImageViewModel
    {
        public string Id { get; set; }
        public string PreviewUrl { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string LazyLoadPlaceholderUrl { get; set; }
    }
}