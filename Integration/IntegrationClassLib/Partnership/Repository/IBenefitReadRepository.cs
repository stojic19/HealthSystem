using Integration.Model;
using Integration.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Partnership.Repository
{
    public interface IBenefitReadRepository : IReadBaseRepository<int, Benefit>
    {
        public IEnumerable<Benefit> GetVisibleBenefits();

        public IEnumerable<Benefit> GetPublishedBenefits();

        public IEnumerable<Benefit> GetRelevantBenefits();
    }
}
