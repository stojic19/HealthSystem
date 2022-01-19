﻿using Microsoft.AspNetCore.Mvc;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO.Shared;
using IntegrationAPI.HttpRequestSenders;

namespace IntegrationAPI.Controllers.Base
{
    public abstract class BaseIntegrationController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly SftpCredentialsDTO _sftpCredentials;
        protected readonly HospitalDTO _hospitalInfo;
        protected readonly string _hospitalBaseUrl;
        protected readonly IHttpRequestSender _httpRequestSender;

        protected BaseIntegrationController(IUnitOfWork uow, IHttpRequestSender sender)
        {
            var ipAdr = "127.0.0.1";
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
            _hospitalBaseUrl = "https://localhost:44303";
            _httpRequestSender = sender;
        }
    }
}
