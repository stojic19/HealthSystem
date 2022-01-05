using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Shared.Repository.Base;
using Integration.Tendering.Model;

namespace Integration.Tendering.Repository
{
    public interface ITenderReadRepository : IReadBaseRepository<int, Tender>
    {
    }
}
