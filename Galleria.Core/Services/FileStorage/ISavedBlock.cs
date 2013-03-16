using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galleria.Core.Services.FileStorage
{
    public interface ISavedBlock
    {
        int ClientFileIndex { get; set; }
        int ClientBlobIndex { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        long Size { get; set; }
    }
}
