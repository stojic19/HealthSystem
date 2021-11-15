using Integration.Model;
using IntegrationAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Adapters
{
    public class PharmacyAdapter
    {
        public static Pharmacy PharmacyDTOToPharmacy(PharmacyDTO dto)
        {
            Pharmacy pharmacy = new Pharmacy();
            pharmacy.City = dto.City;
            pharmacy.BaseUrl = dto.BaseUrl;
            pharmacy.StreetName = dto.StreetName;
            pharmacy.StreetNumber = dto.StreetNumber;
            pharmacy.Name = dto.Name;
            return pharmacy;
        }
    }
}
