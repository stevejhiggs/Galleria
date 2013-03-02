using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galleria.ViewModels
{
    public class ProcessedImageViewModel
    {
        public string Id { get; set; }
        public string PreviewUrl { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}