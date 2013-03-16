using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Galleria.Core.Services.FileStorage
{
    public interface IChunkedFileStorageService : IFileStorageService
    {
        Task<IEnumerable<ISavedBlock>> SaveAsBlocksAsync(HttpRequestMessage httpRequest);
        Task<ISavedFile> AssembleFileFromBlocksAsync(IFileAssemblyRequest assemblyRequest);
    }
}
