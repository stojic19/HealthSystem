using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.SharedModel.Repository
{
    public interface IManagerReadRepository : IReadBaseRepository<int, Manager>
    {
    }
}
