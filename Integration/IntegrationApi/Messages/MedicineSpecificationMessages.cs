using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApi.Messages
{
    public static class MedicineSpecificationMessages
    {
        public static readonly string FileNotFound = "Specification file not found";
        public static readonly string CannotSaveFile = "Failed to save file, error while trying to download from sftp";
        public static readonly string Success = "Pharmacy has sent the specification file to sftp server";
    }
}
