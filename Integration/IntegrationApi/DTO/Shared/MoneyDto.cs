using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.Tendering.Model;

namespace IntegrationApi.DTO.Shared
{
    public class MoneyDto
    {
        public double Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
