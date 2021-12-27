using System;
using System.Collections.Generic;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Model.Enumerations;

namespace Hospital.MedicalRecords.Model
{
    public class Measurements : ValueObject
    {
        
        public double Weight { get; }
        public double Height { get; }

        public Measurements(double weight, double height)
        {
            Weight = weight;
            Height = height;
            Validate();
        }

        private void Validate()
        {
            if (double.IsNegative(Weight) || double.IsNegative(Height)) throw new Exception();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Weight;
            yield return Height;
        }
    }
}
