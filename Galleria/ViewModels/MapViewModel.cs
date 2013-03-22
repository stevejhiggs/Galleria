using System.Collections.Generic;

namespace Galleria.ViewModels
{
    public class MapViewModel
    {
        public double CentreLatitude { get; set; }
        public double CentreLongitude { get; set; }
        public IList<Galleria.ViewModels.ProcessedImageViewModel> Images { get; set; }
        public int ZoomLevel { get; set; }
    }
}