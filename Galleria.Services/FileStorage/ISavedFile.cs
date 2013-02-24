
namespace Galleria.Services.FileStorage
{
    public interface ISavedFile
    {
        //filename that was uploaded
        string Name { get; set; }
        //file name decided on by the storage system
        string Filename { get; set; }
    }
}
