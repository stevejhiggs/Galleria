
namespace Galleria.Core.Services.FileStorage
{
    internal class SavedBlock : ISavedBlock
    {
        public int ClientFileIndex { get; set; }
        public int ClientBlobIndex { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }

        public SavedBlock(int fileIndex, int blockIndex, string name, string path, long size)
        {
            this.ClientFileIndex = fileIndex;
            this.ClientBlobIndex = blockIndex;
            this.Name = name;
            this.Path = path;
            this.Size = size;
        }
    }
}
