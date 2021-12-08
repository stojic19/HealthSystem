using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Shared.Service;
using IntegrationUnitTests.Base;
using Shouldly;
using Xunit;

namespace IntegrationUnitTests
{
    public class FileZipTests : BaseTest
    {
        public FileZipTests(BaseFixture fixture) : base(fixture) { }

        [Fact]
        public void Zip_files()
        {
            string rootFolder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).Parent.FullName;
            string folderPath = Path.Combine(rootFolder, "files");
            MakeFiles(folderPath);

            string zipName = DateTime.Now.Ticks + ".zip";
            string targetPath = Path.Combine(rootFolder, zipName);
            FileZipService fileZipService = new FileZipService();
            fileZipService.FileZip(folderPath, targetPath);
            Directory.EnumerateFileSystemEntries(folderPath).Any().ShouldBe(false);
            File.Exists(targetPath).ShouldBe(true);
            Directory.Delete(folderPath);
            File.Delete(targetPath);
        }

        private void MakeFiles(string folderPath)
        {
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, "file1.txt");
            File.Create(filePath).Close();
        }
    }
}
