using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationApi.Messages
{
    public static class PrescriptionMessages
    {
        public static readonly string NoMedicineInAnyPharmacy = "No pharmacy has the medicine";
        public static readonly string CannotSend = "Cannot send prescription";
    }
}
