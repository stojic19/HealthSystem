using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Service
{
    public class MedicalRecordService
    {
        private readonly IUnitOfWork _uow;

        public MedicalRecordService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Patient GetPatientWithRecord()
        {
            var patientRepo = _uow.GetRepository<IPatientReadRepository>();
            var patient = patientRepo.GetPatientWithCity();
            var medicalRecordRepo = _uow.GetRepository<IMedicalRecordReadRepository>();
            var medicalRecord = medicalRecordRepo.GetMedicalRecordForPatient(patient.MedicalRecordId);
            patient.MedicalRecord = medicalRecord;
            return patient;

        }
    }
}
