using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Galleria.Core.FileStorage
{
    public interface IChunkedFileStorageService : IFileStorageService
   {
        Task<IEnumerable<SavedBlock>> SaveAsBlocksAsync(HttpRequestMessage httpRequest);
        Task<SavedFile> AssembleFileFromBlocksAsync(FileAssemblyRequest assemblyRequest);
    }
}
