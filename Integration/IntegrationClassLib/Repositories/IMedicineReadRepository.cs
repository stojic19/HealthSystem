﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Model;
using Integration.Repositories.Base;

namespace Integration.Repositories
{
    public interface IMedicineReadRepository : IReadBaseRepository<int, Medicine>
    {
        public Medicine GetMedicineByName(string name);
    }
}
