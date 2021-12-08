using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Shared.Service
{
    public class FileZipService
    {

        public FileZipService() { }

        public void FileZip(string sourceFolder, string targetZip)
        {
            ZipFile.CreateFromDirectory(sourceFolder, targetZip);

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sourceFolder);

            foreach (System.IO.FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}