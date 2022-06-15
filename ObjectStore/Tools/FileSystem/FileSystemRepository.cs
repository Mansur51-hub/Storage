
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectStorage.Tools.FileSystem
{
    public class FileSystemRepository : IRepository
    {
        public byte[] GetData(string path)
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                throw new Exception("File does not exist");
            }

            using FileStream fstream = File.OpenRead(path);
            byte[] bytes = new byte[fstream.Length];
            fstream.Read(bytes, 0, bytes.Length);
            fstream.Close();
            return bytes;
        }
    }
}
