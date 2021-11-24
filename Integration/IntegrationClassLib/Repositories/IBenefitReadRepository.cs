using Integration.Model;
using Integration.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Repositories
{
    public interface IBenefitReadRepository : IReadBaseRepository<int, Benefit>
    {
        public IEnumerable<Benefit> GetVisibleBenefits();
    }
}
