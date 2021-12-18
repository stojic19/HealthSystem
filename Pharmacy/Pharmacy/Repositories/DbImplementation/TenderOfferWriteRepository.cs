using Pharmacy.EfStructures;
using Pharmacy.Model;
using Pharmacy.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Repositories.DbImplementation
{
    public class TenderOfferWriteRepository : WriteBaseRepository<TenderOffer>, ITenderOfferWriteRepository
    {
        public TenderOfferWriteRepository(AppDbContext context) : base(context)
        {
        }
    
    }
}
