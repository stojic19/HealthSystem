using Pharmacy.Model;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class HospitalAuthService
    {
        private readonly IUnitOfWork _uow;

        public HospitalAuthService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void ValidateApiKey(Guid apiKey)
        {
            Hospital hospital = _uow.GetRepository<IHospitalReadRepository>().GetAll()
                .FirstOrDefault(hospital => hospital.ApiKey.Equals(apiKey));

            if (hospital == null)
            {
                throw new UnauthorizedAccessException("Invalid API key.");
            }
        }
    }
}
