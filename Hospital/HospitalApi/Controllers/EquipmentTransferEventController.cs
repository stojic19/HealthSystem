using AutoMapper;
using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using Hospital.Services;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]

    public class EquipmentTransferEventController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public EquipmentTransferEventController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost("addEvent")]
        public IActionResult AddNewEquipmentTransferEvent(EquipmentTransferEvent equipmentTransferEvent)
        {
            try
            {
                if (equipmentTransferEvent == null)
                {
                    return BadRequest("Incorrect format sent! Please try again.");
                }

                if (!IsEnteredAmountIsCorrect(equipmentTransferEvent)) {
                    return BadRequest("Incorrect amount entered. Please Try Again!");
                }

                var repo = _uow.GetRepository<IEquipmentTransferEventWriteRepository>();
                EquipmentTransferEvent addedEvent = repo.Add(equipmentTransferEvent);

                if (addedEvent == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Could not insert transfer event in the database.");
                }

                return Ok("Your transfer request has been recorded.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting transfer event in the database.");
            }
        }

        private bool IsEnteredAmountIsCorrect(EquipmentTransferEvent equipmentTransferEvent)
        {
            var roomInventories = _uow.GetRepository<IRoomInventoryReadRepository>().GetAll()
                                  .Where(roomInventory => roomInventory.RoomId == equipmentTransferEvent.InitalRoomId
                                         && roomInventory.InventoryItemId == equipmentTransferEvent.InventoryItemId);

            if (roomInventories.FirstOrDefault().Amount < equipmentTransferEvent.Quantity)
                return false;

            return true;
        }

        [HttpPost]
        public IEnumerable<TimePeriod> GetAvailableTerms(AvailableTermDTO availableTermsDTO)
        {
            var transferingEquipmentService = new TransferingEquipmentService(_uow);
            var timePeriod = new TimePeriod() {
                StartTime = availableTermsDTO.StartDate,
                EndTime = availableTermsDTO.EndDate
            };
            return transferingEquipmentService.GetAvailableTerms(timePeriod, availableTermsDTO.RoomId, availableTermsDTO.Duration);
        }
    }
}
