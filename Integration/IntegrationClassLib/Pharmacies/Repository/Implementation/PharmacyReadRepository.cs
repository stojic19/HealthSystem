using System.Collections.Generic;
using System.Linq;
using Integration.Database.EfStructures;
using Integration.Shared.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Integration.Pharmacies.Repository.Implementation
{
    public class PharmacyReadRepository : ReadBaseRepository<int, Model.Pharmacy>, IPharmacyReadRepository
    {
        public PharmacyReadRepository(AppDbContext context) : base(context)
        {
        }

        public Model.Pharmacy GetByApiKey(string Apikey)
        {
            DbSet<Model.Pharmacy> allPharmacies = GetAll();
            Model.Pharmacy pharmacy = allPharmacies.FirstOrDefault(pharmacy => pharmacy.ApiKey.Equals(Apikey));
            return pharmacy;
        }

        public IEnumerable<Model.Pharmacy> GetByName(string Name)
        {
            DbSet<Model.Pharmacy> allPharmacies = GetAll();
            List<Model.Pharmacy> pharmacies = new List<Model.Pharmacy>();
            foreach(Model.Pharmacy pharmacy in allPharmacies)
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
