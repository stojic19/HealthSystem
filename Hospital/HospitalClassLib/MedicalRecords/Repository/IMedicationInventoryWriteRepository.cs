using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Repository
{
    public interface IMedicationInventoryWriteRepository : IWriteBaseRepository<MedicationInventory>
    {
    }
}
