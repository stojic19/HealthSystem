using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;

namespace IntegrationAPI.Controllers.Base
{
    public abstract class BaseIntegrationController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly SftpCredentialsDTO _sftpCredentials;
        protected readonly HospitalDTO _hospitalInfo;
        protected BaseIntegrationController(IUnitOfWork uow)
        {
            var ipAdr = "192.168.0.22";
            _unitOfWork = uow;
            _sftpCredentials = new SftpCredentialsDTO
            {
                Host = ipAdr,
                Password = "password",
                Username = "tester"
            };
            _hospitalInfo = new HospitalDTO
            {
                Name = "Nasa bolnica",
                StreetName = "Vojvode Stepe",
                StreetNumber = "14",
                CityName = "Novi Sad"
            };
        }
    }
}
