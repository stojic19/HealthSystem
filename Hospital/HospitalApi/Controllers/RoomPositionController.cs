using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomPositionController : ControllerBase
    {

        private readonly IUnitOfWork _uow;

        public RoomPositionController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpPost]
        public IEnumerable<RoomPosition> AddRoomPositions(IEnumerable<RoomPosition> roomPositions)
        {
            var roomPositionRepo = _uow.GetRepository<IRoomPositionWriteRepository>();
            return roomPositionRepo.AddRange(roomPositions);
        }

        [HttpGet]
        public IEnumerable<RoomPosition> GetRoomsByLocation([FromQuery(Name = "floorNumber")] int floorNumber, [FromQuery(Name = "buildingName")] string buildingName)
        {
            var renovationService = new RenovatingRoomsService(_uow);
            renovationService.StartRoomRenovations();
            var roomPositionRepo = _uow.GetRepository<IRoomPositionReadRepository>();
            return roomPositionRepo.GetAll().Include(rp => rp.Room).Where(rp => rp.Room.FloorNumber == floorNumber && rp.Room.BuildingName == buildingName);
        }
    }
}
