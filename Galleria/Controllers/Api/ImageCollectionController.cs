using AutoMapper;
using Galleria.Core.Models;
using Galleria.Core.RavenDb.Indexes;
using Galleria.RavenDb.BaseControllers;
using Galleria.ViewModels;
using Raven.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galleria.Controllers
{
    public class SearchController : RavenBaseApiController
    {
        public async Task<IEnumerable<ProcessedImageViewModel>> Get(string searchText = null)
        {
            IList<StoredImage> results = null;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                results = await RavenSession.Query<SearchIndex.ReduceResult, SearchIndex>()
                   .Where(x => x.SearchQuery == (object)searchText)
                   .As<StoredImage>()
                    .ToListAsync();
            }
            else
            {
                //all images
                results = await RavenSession.Query<StoredImage>().OrderByDescending(i => i.UploadDateTime).Take(1000).ToListAsync();
            }

            IList<ProcessedImageViewModel> existingMedia = new List<ProcessedImageViewModel>();
            foreach (var upload in results)
            {
                var item = Mapper.Map<ProcessedImageViewModel>(upload);
                item.LazyLoadPlaceholderUrl = "/Content/Images/ImagePlaceholder.png";
                existingMedia.Add(item);
            }

            return existingMedia;
        }
    }
}
