using System;
using System.Collections.Generic;
using Integration.Database.EfStructures;
using Integration.Partnership.Model;
using Integration.Shared.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Integration.Partnership.Repository.Implementation
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

        public IEnumerable<Benefit> GetPublishedBenefits()
        {
            IEnumerable<Benefit> allBenefits = GetAll().Include(x => x.Pharmacy);
            List<Benefit> benefits = new List<Benefit>();
            foreach (Benefit benefit in allBenefits)
            {
                if (benefit.Published)
                {
                    benefits.Add(benefit);
                }
            }
            return benefits;
        }

        public IEnumerable<Benefit> GetRelevantBenefits()
        {
            IEnumerable<Benefit> allBenefits = GetPublishedBenefits();
            List<Benefit> benefits = new List<Benefit>();
            foreach (Benefit benefit in allBenefits)
            {
                if (DateTime.Now < benefit.EndTime)
                {
                    benefits.Add(benefit);
                }
            }
            return benefits;
        }
    }
}
