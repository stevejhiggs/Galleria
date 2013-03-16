using System.Linq;
using Galleria.Core.Models;
using Raven.Client.Indexes;

namespace Galleria.Core.RavenDb.Indexes
{
    public class StoredImageSpatialIndex : AbstractIndexCreationTask<StoredImage>
    {
        public StoredImageSpatialIndex()
        {
            Map = images =>
                from i in images
                select new
                {
                    _ = SpatialIndex.Generate(i.Latitude, i.Longitude)
                };
        }
    }
}