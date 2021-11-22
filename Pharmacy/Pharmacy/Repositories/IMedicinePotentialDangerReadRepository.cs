﻿using Pharmacy.Model;
using Pharmacy.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Repositories
{
    public interface IMedicinePotentialDangerReadRepository : IReadBaseRepository<int, MedicinePotentialDanger>
    {
        MedicinePotentialDanger GetMedicinePotentialDangerByName(string name);
    }
}
