using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Repository.PatientPersistance;

namespace ZdravoHospital.GUI.PatientUI.Logics
{
    public class PatientService
    {
        private string username;
        private PatientRepository patientRepository;

        public PatientService(string username)
        {
            this.username = username;
            patientRepository = new PatientRepository();
        }
        public  Patient LoadPatient()
        {
            return patientRepository.GetById(username);
        }

        public void SerializePatient(Patient patient)
        {
            patientRepository.Update(patient);
        }

        public  bool IsTrollDetected()
        {
            Patient patient = LoadPatient();
            return patient.RecentActions >= 5;
        }

        public bool ActionTaken()
        {
            Patient patient = LoadPatient();
            if (patient.RecentActions == 4)
            {
               BlockAccount(patient);
               return false;
            }
            ++patient.RecentActions;
            patientRepository.Update(patient);
            return true;
        }

        private void BlockAccount(Patient patient)
        {
            patient.RecentActions = 5;
            patientRepository.Update(patient);
        }
    }
}
