using AutoMapper;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]

    public class EquipmentTransferEventController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EquipmentTransferEventController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddNewEquipmentTransferEvent(EquipmentTransferEvent equipmentTransferEvent)
        {
            try
            {
                if (equipmentTransferEvent == null)
                {
                    return BadRequest("Incorrect format sent! Please try again.");
                }

                if (!IsEnteredAmountCorrect(equipmentTransferEvent))
                {
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

        private bool IsEnteredAmountCorrect(EquipmentTransferEvent equipmentTransferEvent)
        {
            var roomInventory = _uow.GetRepository<IRoomInventoryReadRepository>()
                .GetByRoomAndInventoryItem(equipmentTransferEvent.InitialRoomId, equipmentTransferEvent.InventoryItemId);
<<<<<<< HEAD

            if (roomInventory.Amount < equipmentTransferEvent.Quantity)
                return false;
=======
>>>>>>> development

            return roomInventory != null && roomInventory.Amount > equipmentTransferEvent.Quantity;
        }

        [HttpPost]
        public IEnumerable<TimePeriodDTO> GetAvailableTerms(AvailableTermDTO availableTermsDTO)
        {
            var availableTermsService = new AvailableTermsService(_uow);
            var timePeriod = new TimePeriod()
            {
                StartTime = availableTermsDTO.StartDate,
                EndTime = availableTermsDTO.EndDate
            };

            var terms = availableTermsService.GetAvailableTerms(timePeriod, availableTermsDTO.InitialRoomId, availableTermsDTO.DestinationRoomId, availableTermsDTO.Duration);
            var availableTerms = new List<TimePeriodDTO>();
            foreach (TimePeriod term in terms)
            {
                string start = term.StartTime.ToString("g");
                string end = term.EndTime.ToString("g");
                TimePeriodDTO dto = new TimePeriodDTO()
                {
                    StartDate = start,
                    EndDate = end
                };
                availableTerms.Add(dto);
            }

            return availableTerms;
        }

        [HttpGet]
        public IEnumerable<EquipmentTransferEvent> GetTransferEventsByRoom(int roomId)
        {
            var transferEventRepo = _uow.GetRepository<IEquipmentTransferEventReadRepository>();

            return transferEventRepo.GetAll()
                .Where(transfer => transfer.DestinationRoomId == roomId ||
                                    transfer.InitialRoomId == roomId);
        }

        [HttpPost]
        public IActionResult CancelEquipmentTransferEvent(EquipmentTransferEventDto transferEventDTO)
        {
            try
            {
                if (transferEventDTO == null)
                {
                    return BadRequest("Incorrect format sent! Please try again.");
                }

                var cancellingEventsService = new CancellingEventsService(_uow);
                cancellingEventsService.CancelEquipmentTransferEvent(_mapper.Map<EquipmentTransferEvent>(transferEventDTO));

                return Ok("Your transfer event has been canceled.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error cancelling transfer event.");
            }
        }

    }
}