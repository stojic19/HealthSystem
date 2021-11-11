using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Integration.Repositories.DbImplementation
{
    public class PharmacyReadRepository : ReadBaseRepository<int, Pharmacy>, IPharmacyReadRepository
    {
        public PharmacyReadRepository(AppDbContext context) : base(context)
        {
        }

        public Pharmacy GetByApiKey(string Apikey)
        {
            DbSet<Pharmacy> allPharmacies = GetAll();
            Pharmacy pharmacy = allPharmacies.FirstOrDefault(pharmacy => pharmacy.ApiKey.Equals(Apikey));
            return pharmacy;
        }

        public IEnumerable<Pharmacy> GetByName(string Name)
        {
            DbSet<Pharmacy> allPharmacies = GetAll();
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            foreach(Pharmacy pharmacy in allPharmacies)
            {
                if (pharmacy.Name.Equals(Name))
                {
                    pharmacies.Add(pharmacy);
                }
            }
            return pharmacies;
        }
    }
}
