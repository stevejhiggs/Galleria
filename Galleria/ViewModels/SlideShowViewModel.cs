using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galleria.ViewModels
{
    public class SlideShowViewModel
    {
        public IList<ProcessedImageViewModel> Images { get; set; }
        public int? StartNumber { get; set; }
    }
}