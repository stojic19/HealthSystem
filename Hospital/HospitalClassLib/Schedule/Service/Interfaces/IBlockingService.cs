using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.MedicalRecords.Model;

namespace Hospital.Schedule.Service.Interfaces
{
    public interface IBlockingService
    {
        public List<Patient> GetMaliciousPatients();
        public void BlockPatient(string userName);
    }
}
