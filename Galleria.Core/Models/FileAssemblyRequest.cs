
using Galleria.Core.Services.FileStorage;

namespace Galleria.Core.Models
{
    public class FileAssemblyRequest
    {
        public string Filename { get; set; }
        public string[] Blocks { get; set; }
    }
}
