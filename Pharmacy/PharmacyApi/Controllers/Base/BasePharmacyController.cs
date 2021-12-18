using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Repositories;
using Pharmacy.Repositories.Base;
using PharmacyApi.ConfigurationMappers;
using PharmacyApi.DTO;

namespace PharmacyApi.Controllers.Base
{
    public abstract class BasePharmacyController : ControllerBase
    {
        protected readonly IUnitOfWork UoW;
        protected readonly PharmacyDetails PharmacyDetails;
        protected SftpCredentialsDTO _sftpCredentials = new SftpCredentialsDTO
        {
            Host = "192.168.0.13",
            Password = "password",
            Username = "tester"
        };

    protected BasePharmacyController(IUnitOfWork uow, PharmacyDetails details)
        {
            UoW = uow;
            PharmacyDetails = details;
        }

        protected bool IsApiKeyValid(Guid apiKey)
        {
            var hospital = UoW.GetRepository<IHospitalReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.ApiKey == apiKey);

            if (hospital == null)
            {
                ModelState.AddModelError("ApiKey", "Hospital was not registered in the system!");
            }

            return hospital != null;
        }
    }
}
