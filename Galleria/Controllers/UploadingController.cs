
using Galleria.Core.Services.FileStorage;
using Galleria.RavenDb.BaseControllers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Galleria.Controllers
{
    public class UploadingController : RavenBaseApiController
    {
        IChunkedFileStorageService ChunkedFileStorageService;

        public UploadingController(IChunkedFileStorageService chunkedFileStorageService)
        {
            ChunkedFileStorageService = chunkedFileStorageService;
        }

        public Task<IEnumerable<ISavedBlock>> Post()
        {
            try
            {
                return ChunkedFileStorageService.SaveAsBlocksAsync(Request);
            }
            catch (ArgumentException)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }
    }

    

}
