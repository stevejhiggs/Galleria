using System.Linq;
using Raven.Client.Indexes;
using Galleria.Core.FileStorage;

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