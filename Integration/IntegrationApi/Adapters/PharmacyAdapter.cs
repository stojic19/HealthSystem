using Integration.Pharmacies.Model;
using IntegrationAPI.DTO;

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
