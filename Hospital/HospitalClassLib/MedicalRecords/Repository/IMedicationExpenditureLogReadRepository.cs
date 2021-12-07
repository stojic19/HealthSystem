using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;

namespace Hospital.MedicalRecords.Repository
{
    public interface IMedicationExpenditureLogReadRepository : IReadBaseRepository<int, MedicationExpenditureLog>
    {
        public IEnumerable<MedicationExpenditureLog> GetMedicationExpenditureLogsInTimePeriod(TimePeriod timePeriod);
    }
}
