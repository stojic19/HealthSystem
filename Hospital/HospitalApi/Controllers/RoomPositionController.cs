using Hospital.GraphicalEditor.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;


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
