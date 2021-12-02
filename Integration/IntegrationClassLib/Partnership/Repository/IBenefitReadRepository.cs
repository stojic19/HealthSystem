using System.Collections.Generic;
using Integration.Partnership.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Partnership.Repository
{
    public interface IBenefitReadRepository : IReadBaseRepository<int, Benefit>
    {
        public IEnumerable<Benefit> GetVisibleBenefits();

        public IEnumerable<Benefit> GetPublishedBenefits();

        public IEnumerable<Benefit> GetRelevantBenefits();
    }
}
