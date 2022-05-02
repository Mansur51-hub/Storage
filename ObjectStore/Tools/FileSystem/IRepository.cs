using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorage.Tools.FileSystem
{
    public interface IRepository
    {
        byte[] GetData(String path);
    }
}
