using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApi.Messages
{
    public class PharmacyMessages
    {
        public static readonly string WrongId = "Pharmacy with that ID not found";
        public static readonly string NotRegistered = "Pharmacy not registered";
        public static readonly string MedicineNotFound = "Pharmacy does not have medicine with given name!";
        public static readonly string FileNotSent = "Pharmacy failed to send file via sftp";
        public static readonly string CannotReach = "Failed to reach pharmacy!";
        public static readonly string PharmacyUpdated = "Pharmacy info updated!";
        public static readonly string PharmacyImageUploaded = "Pharmacy image updated!";
    }
}
