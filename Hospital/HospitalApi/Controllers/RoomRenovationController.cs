using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Repository.Base;
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
            this._uow = uow;
        }

        [HttpGet]
        public IEnumerable<RoomRenovationEvent> GetRenovationsByRoom(int roomId)
        {
            var roomRenovationRepo = _uow.GetRepository<IRoomRenovationEventReadRepository>();
            return roomRenovationRepo.GetAll()
                .Where(renovation => renovation.RoomId == roomId ||
                                     renovation.MergeRoomId == roomId);
        }
    }
}
