using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Model;

namespace Pharmacy.Extensions
{
    public static class HospitalExtensions
    {
        public static bool Equals(this Hospital hospital, Hospital otherHospital)
        {
            return hospital.Name == otherHospital.Name &&
                   hospital.ApiKey == otherHospital.ApiKey &&
                   hospital.StreetName == otherHospital.StreetName &&
                   hospital.StreetNumber == otherHospital.StreetNumber;
        }
    }
}
