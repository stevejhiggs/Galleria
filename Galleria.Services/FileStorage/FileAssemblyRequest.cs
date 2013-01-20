
namespace Galleria.Services.FileStorage
{
    public class FileAssemblyRequest : IFileAssemblyRequest
    {
        public string Filename { get; set; }
        public string[] Blocks { get; set; }
    }
}
