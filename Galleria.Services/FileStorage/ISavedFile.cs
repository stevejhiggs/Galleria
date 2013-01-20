
namespace Galleria.Services.FileStorage
{
    public interface ISavedFile
    {
        //filename that was uploaded
        string OriginalFileName { get; set; }
        //file name decided on by the storage system
        string StorageFilename { get; set; }
        //uri for the file on the storage system
        string StorageUri { get; set; }
    }
}
