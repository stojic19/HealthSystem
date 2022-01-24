using Integration.Pharmacies.Model;
using Integration.Pharmacies.Service;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;
using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System;
using IntegrationAPI.Controllers.Base;
using IntegrationAPI.HttpRequestSenders;
using Integration.Shared.Repository;

namespace IntegrationAPI.Controllers.Notifications
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : BaseIntegrationController
    {
        public NotificationController(IUnitOfWork uow, IHttpRequestSender requestSender) : base(uow, requestSender) { }

        [HttpGet]
        public IEnumerable<Notification> GetNotifications()
        {
            List<Notification> retVal = new List<Notification>();
            var notifications = _unitOfWork.GetRepository<INotificationReadRepository>().GetAll();
            foreach (var notification in notifications)
            {
                if(!notification.Seen)
                    retVal.Add(notification);
            }

            return retVal;
        }

        [HttpGet("{id:int}"), Produces("application/json")]
        public IActionResult Seen(int id)
        {
            var notification = _unitOfWork.GetRepository<INotificationReadRepository>().GetById(id);
            if (notification == null) return NotFound("Notification does not exist");
            notification.Seen = true;
            _unitOfWork.GetRepository<INotificationWriteRepository>().Update(notification);

            return Ok("Notification seen.");
        }
    }
}