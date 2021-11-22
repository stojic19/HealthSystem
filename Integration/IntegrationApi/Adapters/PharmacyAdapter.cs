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
        public static Pharmacy PharmacyDTOToPharmacy(PharmacyUrlDTO urlDto)
        {
            Pharmacy pharmacy = new Pharmacy();
            pharmacy.BaseUrl = urlDto.BaseUrl;
            return pharmacy;
        }
    }
}
