using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApi.Messages
{
    public class MedicineMessages
    {
        public static readonly string InvalidQuantity = "Invalid quantity, it must be positive";
        public static readonly string DidNotReceive = "Pharmacy failed to receive request! Try again";
        public static readonly string CouldNotAddToHospital = "Hospital could not add received medicine";
    }
}
