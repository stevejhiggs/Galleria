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
        /// Save a single byte stream to a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        /// <param name="fileContents"></param>
        /// <returns></returns>
        ISavedFile SaveFileWithoutChunking(string fileName, FileType fileType, byte[] fileContents);

        /// <summary>
        /// Get a files uri from the storage system
        /// </summary>
        /// <param name="storageFileName"></param>
        /// <returns></returns>
        string GetFileStorageUri(string fileName, FileType fileType);

        /// <summary>
        /// Get back a file for further processing
        /// </summary>
        /// <param name="file">The file to retrieve</param>
        /// <returns></returns>
        byte[] RetrieveFileContents(ISavedFile file, FileType fileType);
    }
}
