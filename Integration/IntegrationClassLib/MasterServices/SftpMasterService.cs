using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Model;
using Newtonsoft.Json;
using Renci.SshNet;

namespace Integration.MasterServices
{
    public class SftpMasterService
    {
        public void SaveMedicineReportToSftpServer(MedicineConsumptionReport consumptionReport)
        {
            string path = "MedicineReports" + Path.DirectorySeparatorChar + "Report-" + 
                          consumptionReport.createdDate.Ticks.ToString() + ".txt";
            try
            {
                SaveFile(consumptionReport, path);
            }
            catch (Exception e)
            {
                throw e;
            }
            try
            {
                SaveToSftp(path);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void SaveFile(MedicineConsumptionReport consumptionReport, string path)
        {
            StreamWriter fileSaveStream = new StreamWriter(path);
            string jsonString = JsonConvert.SerializeObject(consumptionReport);
            fileSaveStream.Write(jsonString);
            fileSaveStream.Close();
        }

        private void SaveToSftp(string path)
        {
            SftpClient sftpClient = new SftpClient(new PasswordConnectionInfo("192.168.0.13", "tester", "password"));
            sftpClient.Connect();
            Stream fileStream = File.OpenRead(path);
            string filePath = Path.GetFileName(path);
            sftpClient.UploadFile(fileStream, filePath);
            sftpClient.Disconnect();
            fileStream.Close();
        }

        /*public bool TestSftpDownload()
        {
            SftpClient sftpClient = new SftpClient(new PasswordConnectionInfo("192.168.0.13", "tester", "password"));
            sftpClient.Connect();
            Stream fileStream = File.OpenWrite(@"../../../../SFTP" + Path.DirectorySeparatorChar + "test.txt");
            sftpClient.DownloadFile("test.txt", fileStream);
            return true;
        }*/
    }
}
