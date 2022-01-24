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
using Microsoft.EntityFrameworkCore;

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

        //[Authorize(Roles = "Manager")]
        [HttpGet]
        public IEnumerable<ScheduledEvent> GetScheduledEventsByRoom(int roomId)
        {
            var scheduleRepo = _uow.GetRepository<IScheduledEventReadRepository>();

            return scheduleRepo.GetAll().Include(x => x.Patient).Include(x => x.Doctor).Include(x => x.Room)
                .Where(scheduledEvent => !scheduledEvent.IsCanceled &&
                                        !scheduledEvent.IsDone &&
                                        scheduledEvent.RoomId == roomId);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IEnumerable<Room> GetRoomsByLocation([FromQuery(Name = "floorNumber")] int floorNumber, [FromQuery(Name = "buildingName")] string buildingName)
        {
            var renovationService = new RenovatingRoomsService(_uow);
            renovationService.StartRoomRenovations();
            var roomRepo = _uow.GetRepository<IRoomReadRepository>();
            return roomRepo.GetAll()
                           .Where(r => r.FloorNumber == floorNumber &&
                                    r.BuildingName == buildingName);
        }
    }
}
