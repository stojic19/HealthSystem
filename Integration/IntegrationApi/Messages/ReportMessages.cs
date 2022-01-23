using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApi.Messages
{
    public class ReportMessages
    {
        public static readonly string Empty = "Report is empty!";
        public static readonly string SftpError = "Failed to contact sftp server";
        public static readonly string ReportSent = "Report sent to pharmacies";
    }
}
