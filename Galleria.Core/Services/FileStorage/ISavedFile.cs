
namespace Galleria.Core.Services.FileStorage
{
    public interface ISavedFile
    {
        //filename that was uploaded
        string UploadedFileName { get; set; }
        //file name decided on by the storage system
        string StorageFileName { get; set; }
    }
}
