﻿using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomInventoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public RoomInventoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public IActionResult AddInventoryItems(IEnumerable<RoomInventory> roomInventory)
        {
            var roomInventoryRepo = _uow.GetRepository<IRoomInventoryWriteRepository>();
            return Ok(roomInventoryRepo.AddRange(roomInventory));
        }

        [HttpGet]
        public IActionResult GetRoomInventory([FromQuery(Name = "roomId")] int roomId)
        {
            var transferingService = new TransferingEquipmentService(_uow);
            transferingService.StartEquipmentTransferEvent();
            var roomInventoryRepo = _uow.GetRepository<IRoomInventoryReadRepository>();
            return Ok(
            roomInventoryRepo.GetAll()
                             .Include(r => r.Room)
                             .Where(roomInventory => roomInventory.RoomId == roomId));
        }

        [HttpGet]
        public IActionResult GetHospitalInventory()
        {
            var transferingService = new TransferingEquipmentService(_uow);
            transferingService.StartEquipmentTransferEvent();
            var roomInventoryRepo = _uow.GetRepository<IRoomInventoryReadRepository>();
            return Ok(
            roomInventoryRepo.GetAll()
                             .Include(r => r.Room)
                             .Include(r => r.InventoryItem));
        }

        [HttpGet]
        public IActionResult FindByInventoryItemName([FromQuery(Name = "inventoryItemName")] string inventoryItemName)
        {
            var roomInventoryRepo = _uow.GetRepository<IRoomInventoryReadRepository>();
            if (inventoryItemName == null)
            {
                return BadRequest();
            }
            return Ok(roomInventoryRepo.GetAll()
                                       .Include(r => r.Room)
                                       .Include(r => r.InventoryItem)
                                       .Where(ri => ri.InventoryItem.Name.ToLower()
                                       .Contains(inventoryItemName.ToLower())));
        }

        [HttpGet]
        public IActionResult GetRoomInventoryById([FromQuery(Name = "id")] int id)
        {
            var repo = _uow.GetRepository<IRoomInventoryReadRepository>();

            return Ok(repo.GetAll()
                          .Include(x => x.Room)
                          .Include(x => x.InventoryItem)
                          .Where(ri => ri.Id == id));
        }

        [HttpGet]
        public IActionResult GetRoomInventoryAmount(int roomId, int itemId)
        {
            var roomInventoryRepo = _uow.GetRepository<IRoomInventoryReadRepository>();
            IEnumerable<RoomInventory> roomInventories = new List<RoomInventory>();

            roomInventories = roomInventoryRepo.GetAll()
                .Where(ri => ri.RoomId == roomId && ri.InventoryItemId == itemId);

            if (roomInventories.Count() == 0)
            {
                return Ok(0);
            }
            else
            {
                return Ok(roomInventories.First().Amount);
            }
        }

    }
}
