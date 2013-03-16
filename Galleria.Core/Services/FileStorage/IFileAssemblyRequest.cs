
namespace Galleria.Core.Services.FileStorage
{
    public interface IFileAssemblyRequest
    {
        string[] Blocks { get; set; }
        string Filename { get; set; }
    }
}
