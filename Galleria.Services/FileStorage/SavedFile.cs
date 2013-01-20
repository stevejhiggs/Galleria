
namespace Galleria.Services.FileStorage
{
    public class SavedFile : ISavedFile
    {
        public string OriginalFileName { get; set; }
        public string StorageFilename { get; set; }
        public string StorageUri { get; set; }
    }
}
