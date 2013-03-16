
using Galleria.Core.Services.FileStorage;

namespace Galleria.Core.Models
{
    public class FileAssemblyRequest : IFileAssemblyRequest
    {
        public string Filename { get; set; }
        public string[] Blocks { get; set; }
    }
}
