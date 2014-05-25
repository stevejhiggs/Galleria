using AutoMapper;
using Galleria.Core.FileStorage;
using Galleria.Core.RavenDb.Indexes;
using Galleria.RavenDb.BaseControllers;
using Galleria.ViewModels;
using Raven.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Galleria.Controllers.Api
{
	[RoutePrefix("api")]
    public class ImageCollectionController : RavenBaseApiController
    {
		[HttpGet]
		[Route("Images/{searchText?}")]
        public async Task<IEnumerable<ProcessedImageViewModel>> SearchByName(string searchText = null)
        {
            IList<StoredImage> results = null;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                results = await RavenSession.Query<SearchIndex.ReduceResult, SearchIndex>()
                    .Search(x => x.SearchQuery,string.Format("{0}*",searchText), escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
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
