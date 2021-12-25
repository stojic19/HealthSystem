using System;
using System.Collections.Generic;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;

namespace Hospital.MedicalRecords.Model
{
    public class PatientCharacteristics : ValueObject
    {
        public BloodType BloodType { get; }
        public double Weight { get; }
        public double Height { get; }
        public JobStatus JobStatus { get; }


        public PatientCharacteristics(BloodType bloodType, double weight, double height, JobStatus jobStatus)
        {
            BloodType = bloodType;
            Weight = weight;
            Height = height;
            JobStatus = jobStatus;
            Validate();
        }

        private void Validate()
        {
            if (double.IsNegative(Weight) || double.IsNegative(Height) || !Enum.IsDefined<BloodType>(BloodType) || !Enum.IsDefined<JobStatus>(JobStatus)) throw new Exception();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return BloodType;
            yield return Weight;
            yield return Height;
            yield return JobStatus;
        }
    }
}
