using Hospital.MedicalRecords.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class RoomController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public RoomController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IEnumerable<Room> AddRooms(IEnumerable<Room> rooms)
        {
            var roomRepo = _uow.GetRepository<IRoomWriteRepository>();
            return roomRepo.AddRange(rooms);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut]
        public Room UpdateRoom(Room room)
        {
            var roomRepo = _uow.GetRepository<IRoomWriteRepository>();
            return roomRepo.Update(room);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult FindByNameAndBuildingName([FromQuery(Name = "name")] string name, [FromQuery(Name = "buildingName")] string buildingName)
        {
            
            if (name == null || buildingName == null)
            {

                return BadRequest();
            }

            var roomRepo = _uow.GetRepository<IRoomReadRepository>();
            return Ok(roomRepo.GetAll().Where(room => room.Name.ToLower().Contains(name.ToLower()) && room.BuildingName.Contains(buildingName)));
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IEnumerable<Room> GetAllRooms()
        {
            var renovationService = new RenovatingRoomsService(_uow);
            renovationService.StartRoomRenovations();
            var roomRepo = _uow.GetRepository<IRoomReadRepository>();
            return roomRepo.GetAll();
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IEnumerable<ScheduledEvent> GetScheduledEventsByRoom(int roomId)
        {
            var scheduleRepo = _uow.GetRepository<IScheduledEventReadRepository>();

            return scheduleRepo.GetAll()
                .Select(scheduledEvent => new ScheduledEvent()
                {
                    Id = scheduledEvent.Id,
                    StartDate = scheduledEvent.StartDate,
                    EndDate = scheduledEvent.EndDate,
                    IsCanceled = scheduledEvent.IsCanceled,
                    IsDone = scheduledEvent.IsDone,
                    RoomId = scheduledEvent.RoomId,
                    Room = new Room()
                    {
                        Id = scheduledEvent.Room.Id,
                        Name = scheduledEvent.Room.Name,
                        BuildingName = scheduledEvent.Room.BuildingName
                    },
                    Doctor = new Doctor()
                    {
                        FirstName = scheduledEvent.Doctor.FirstName,
                        LastName = scheduledEvent.Doctor.LastName
                    },
                    Patient = new Patient()
                    {
                        FirstName = scheduledEvent.Patient.FirstName,
                        LastName = scheduledEvent.Patient.LastName
                    },
                })
                .Where(scheduledEvent => !scheduledEvent.IsCanceled &&
                                        !scheduledEvent.IsDone &&
                                        scheduledEvent.RoomId == roomId);
        }
    }
}
