
namespace Galleria.Core.Services.FileStorage
{
    public class SavedFile : ISavedFile
    {
        public string UploadedFileName { get; set; }
        public string StorageFileName { get; set; }
    }
}
