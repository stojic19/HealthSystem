using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Base;
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
    public class RoomInventoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public RoomInventoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /*[HttpPost]
        public IActionResult AddInventoryItems(IEnumerable<RoomInventory> roomInventory)
        {
            var roomInventoryRepo = _uow.GetRepository<IRoomInventoryWriteRepository>();
            return Ok(roomInventoryRepo.AddRange(roomInventory));
        }*/

        [HttpGet]
        public IActionResult GetRoomInventory([FromQuery(Name = "roomId")] int roomId)
        {
       
           var roomInventoryRepo = _uow.GetRepository<IRoomInventoryReadRepository>();
            return Ok(
            roomInventoryRepo.GetAll().Select(ri => new RoomInventory()
            {
                Id = ri.Id,
                Amount = ri.Amount,
                InventoryItemId = ri.InventoryItemId,
                RoomId = ri.RoomId,
                InventoryItem = new InventoryItem()
                {
                    Id = ri.InventoryItem.Id,
                    InventoryItemType = ri.InventoryItem.InventoryItemType,
                    Name = ri.InventoryItem.Name
                }
            }
            )
            .Where(roomInventory => roomInventory.RoomId == roomId));

        }

    }
}
