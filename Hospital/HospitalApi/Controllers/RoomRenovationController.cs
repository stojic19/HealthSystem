using Hospital.GraphicalEditor.Repository;
using Hospital.GraphicalEditor.Service;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Service;
using Hospital.SharedModel.Model.Wrappers;
using Hospital.SharedModel.Repository.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomRenovationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public RoomRenovationController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IActionResult GetSurroundingRoomsForRoom([FromQuery(Name = "roomId")] int roomId)
        {
            var surroundingRoomsService = new FindingSurroundingRoomsService(_uow);
            var roomPosition = _uow.GetRepository<IRoomPositionReadRepository>().GetByRoom(roomId);
            if (roomId <= 0)
            {

                return BadRequest();
            }
            return Ok(surroundingRoomsService.GetSurroundingRooms(roomPosition));
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

        [HttpPost]
        public IActionResult AddNewRoomRenovationEvent(RoomRenovationEvent roomRenovationEvent)
        {
            try
            {
                if (roomRenovationEvent == null)
                {
                    return BadRequest("Incorrect format sent! Please try again.");
                }


                var repo = _uow.GetRepository<IRoomRenovationEventWriteRepository>();
                RoomRenovationEvent addedEvent = repo.Add(roomRenovationEvent);

                if (addedEvent == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Could not insert event in the database.");
                }

                return Ok("Your room renovation request has been recorded.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting event event in the database.");
            }
        }
    }
}
