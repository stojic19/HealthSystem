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
    public abstract class BaseSftpController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly SftpCredentialsDTO sftpCredentials;
        protected BaseSftpController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
            sftpCredentials = new SftpCredentialsDTO
            {
                Host = "192.168.0.13",
                Password = "password",
                Username = "tester"
            };
        }
    }
}
