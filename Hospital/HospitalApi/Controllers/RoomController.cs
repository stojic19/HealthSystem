using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class RoomController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public RoomController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet]
        public IEnumerable<Room> GetRoomsByLocation([FromQuery(Name = "floorNumber")] int floorNumber, [FromQuery(Name = "buildingName")] string buildingName)
        {
            var roomRepo = _uow.GetRepository<IRoomReadRepository>();
            return roomRepo.GetAll().Where(x => x.FloorNumber == floorNumber && x.BuildingName == buildingName);
        }

        //[HttpPost]
        //public IEnumerable<Room> AddRooms(IEnumerable<Room> rooms)
        //{
        //    var roomRepo = _uow.GetRepository<IRoomWriteRepository>();
        //    return roomRepo.AddRange(rooms);
        //}

        [HttpPut]
        public Room UpdateRoom(Room room)
        {
            var roomRepo = _uow.GetRepository<IRoomWriteRepository>();
            return roomRepo.Update(room);
        }


        [HttpGet("find")]      
        public IActionResult FindByName([FromQuery(Name = "roomName")] string roomName, [FromQuery(Name = "buildingName")] string buildingName)
        {
            var roomRepo = _uow.GetRepository<IRoomReadRepository>();
            if (roomName==null)
            {
                return Ok(roomRepo.GetAll().Where(x => x.BuildingName == buildingName));
            }
            return Ok(roomRepo.GetAll().Where(x => x.Name.ToLower().Contains(roomName.ToLower()) && x.BuildingName == buildingName));
        }
    }
}
