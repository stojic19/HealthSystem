﻿using System;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hospital.MedicalRecords.Model
{
    public class MedicalRecord
    {
        public int Id { get; private set; }
        [Required]
        public Measurements Measurements { get; private set; }
        [Required]
        public BloodType BloodType { get; private set; }
        [Required]
        public JobStatus JobStatus { get; private set; }
        [Required]
        public int DoctorId { get; private set; }
        public Doctor Doctor { get; }
        public IEnumerable<Allergy> Allergies { get; }

        public MedicalRecord( Measurements measurements, BloodType bloodType, JobStatus jobStatus, int doctorId, IEnumerable<Allergy> allergies)
        {
            Measurements = measurements;
            BloodType = bloodType;
            JobStatus = jobStatus;
            DoctorId = doctorId;
            Allergies = allergies;
            Validate();
        }

        public MedicalRecord()
        {
        }

        private void Validate()
        {
            if (DoctorId <= 0) throw new Exception();
        }

        public IEnumerable<Allergy> GetAllergies()
        {
            return new List<Allergy>(Allergies);
        }

        public void AddNewAllergy(Allergy newAllergy)
        {
            Allergies.ToList().Add(newAllergy);
            Validate();
        }
    }
}
