
namespace Galleria.Core.Services.FileStorage
{
    public class SavedFile
    {
        //filename that was uploaded
        public string UploadedFileName { get; set; }
        //file name decided on by the storage system
        public string StorageFileName { get; set; }
        //indicates the group of the file, may or may be not used by the storage system
        public string Category { get; set; }
    }
}
