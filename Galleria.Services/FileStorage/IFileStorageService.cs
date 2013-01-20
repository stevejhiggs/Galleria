using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Galleria.Services.FileStorage
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Used if the storage service does not support chunking or if the file would fit in a single chunk
        /// </summary>
        /// <param name="httpContent"></param>
        /// <returns></returns>
        Task<IEnumerable<ISavedFile>> SaveFilesWithoutChunkingAsync(HttpRequestMessage httpRequest);
    }
}
