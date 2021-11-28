using System.Collections.Generic;
using Integration.Pharmacies.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacies.Repository
{
    public interface IPharmacyReadRepository : IReadBaseRepository<int, Pharmacy>
    {
        public IEnumerable<Pharmacy> GetByName(string Name);
        public Pharmacy GetByApiKey(string Apikey);
    }
}
