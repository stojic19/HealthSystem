using Integration.EfStructures;
using Integration.Model;
using Integration.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Repositories.DbImplementation
{
    public class BenefitReadRepository : ReadBaseRepository<int, Benefit>, IBenefitReadRepository
    {
        public BenefitReadRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Benefit> GetVisibleBenefits()
        {
            IEnumerable<Benefit> allBenefits = GetAll().Include(x => x.Pharmacy);
            List<Benefit> benefits = new List<Benefit>();
            foreach (Benefit benefit in allBenefits)
            {
                if (!benefit.Hidden)
                {
                    benefits.Add(benefit);
                }
            }
            return benefits;
        }
    }
}
