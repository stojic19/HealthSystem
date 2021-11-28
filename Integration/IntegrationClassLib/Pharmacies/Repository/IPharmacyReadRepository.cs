using System.Collections.Generic;
using Integration.Shared.Repository.Base;

namespace Integration.Pharmacies.Repository
{
    public interface IPharmacyReadRepository : IReadBaseRepository<int, Model.Pharmacy>
    {
        public IEnumerable<Model.Pharmacy> GetByName(string Name);
        public Model.Pharmacy GetByApiKey(string Apikey);
    }
}
