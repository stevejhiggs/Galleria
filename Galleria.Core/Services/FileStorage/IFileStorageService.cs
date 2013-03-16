using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Galleria.Core.Services.FileStorage
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Used if the storage service does not support chunking or if the file would fit in a single chunk
        /// </summary>
        /// <param name="httpContent"></param>
        /// <returns></returns>
        Task<IEnumerable<ISavedFile>> SaveFilesWithoutChunkingAsync(HttpRequestMessage httpRequest);

        /// <summary>
        /// Get a files uri from the storage system
        /// </summary>
        /// <param name="storageFileName"></param>
        /// <returns></returns>
        string GetFileStorageUri(ISavedFile file, FileType fileType);

        /// <summary>
        /// Get back a file for further processing
        /// </summary>
        /// <param name="id">The image id</param>
        /// <returns></returns>
        //Task<Stream> RetrieveFileContents(string id);
    }
}
