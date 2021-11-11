using Integration.Model;
using Integration.Repositories.Base;
using System.Collections.Generic;

namespace Integration.Repositories
{
    public interface IPharmacyReadRepository : IReadBaseRepository<int, Pharmacy>
    {
        public IEnumerable<Pharmacy> GetByName(string Name);
        public Pharmacy GetByApiKey(string Apikey);
    }
}
