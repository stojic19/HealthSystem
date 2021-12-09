using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

        [HttpPost]
        public IEnumerable<Room> AddRooms(IEnumerable<Room> rooms)
        {
            var roomRepo = _uow.GetRepository<IRoomWriteRepository>();
            return roomRepo.AddRange(rooms);
        }

        [HttpPut]
        public Room UpdateRoom(Room room)
        {
            var roomRepo = _uow.GetRepository<IRoomWriteRepository>();
            return roomRepo.Update(room);
        }


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

        [HttpGet]
        public IEnumerable<Room> GetAllRooms()
        {
            var roomRepo = _uow.GetRepository<IRoomReadRepository>();
            return roomRepo.GetAll();
        }

    }
}
