using AutoMapper;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Model.Enumerations;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalIntegrationTests
{
    public class CancellingEventTests : BaseTest
    {
        public CancellingEventTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Transfer_event_should_be_canceled()
        {
            var sourceRoom = InsertRoom("SR-2");
            var destinationRoom = InsertRoom("SR-3");
            var inventoryItem = InsertInventoryItem("Chair");
            var roomInventoryItem = InsertInventoryInRoom(sourceRoom.Id, inventoryItem.Id, 4);
            var transferRequest = InsertTransferRequest(new DateTime(2025, 11, 22, 0, 0, 0), sourceRoom.Id, destinationRoom.Id, inventoryItem.Id);

            var content = GetContent(new EquipmentTransferEventDto()
            {
                Id = transferRequest.Id,
                StartDate = transferRequest.StartDate,
                EndDate = transferRequest.EndDate,
                InitialRoomId = transferRequest.InitialRoomId,
                DestinationRoomId = transferRequest.DestinationRoomId,
                InventoryItemId = transferRequest.InventoryItemId,
                Quantity = transferRequest.Quantity
            });

            var response = await Client.PostAsync(BaseUrl + "api/EquipmentTransferEvent/CancelEquipmentTransferEvent", content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.ShouldNotBeNull();

            var canceledRequest = UoW.GetRepository<IEquipmentTransferEventReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.StartDate == transferRequest.StartDate &&
                                x.EndDate == transferRequest.EndDate &&
                                x.InitialRoomId == transferRequest.InitialRoomId &&
                                x.DestinationRoomId == transferRequest.DestinationRoomId &&
                                x.InventoryItemId == transferRequest.InventoryItemId);

            canceledRequest.ShouldBeNull();
        }

        private InventoryItem InsertInventoryItem(string name)
        {
            var inventoryItem = UoW.GetRepository<IInventoryItemReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == name);

            if (inventoryItem == null)
            {
                inventoryItem = new InventoryItem()
                {
                    Name = name,
                    InventoryItemType = InventoryItemType.Dynamic
                };

                UoW.GetRepository<IInventoryItemWriteRepository>().Add(inventoryItem);
            }

            return inventoryItem;
        }

        private Room InsertRoom(string name)
        {
            var room = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == name);

            if (room == null)
            {
                room = new Room()
                {
                    Name = name,
                    Description = "Room for storage",
                    Width = 7,
                    Height = 8.5,
                    FloorNumber = 1,
                    BuildingName = "Building 2",
                    RoomType = RoomType.Storage,
                };

                UoW.GetRepository<IRoomWriteRepository>().Add(room);
            }

            return room;
        }

        private RoomInventory InsertInventoryInRoom(int roomId, int inventoryId, int amount)
        {
            var roomInventory = UoW.GetRepository<IRoomInventoryReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.InventoryItemId == inventoryId && x.RoomId == roomId);

            if (roomInventory == null)
            {
                roomInventory = new RoomInventory()
                {
                    Amount = amount,
                    InventoryItemId = inventoryId,
                    RoomId = roomId
                };
                UoW.GetRepository<IRoomInventoryWriteRepository>().Add(roomInventory);
            }
            else if (roomInventory.Amount < amount)
            {
                roomInventory.Amount = amount;
                UoW.GetRepository<IRoomInventoryWriteRepository>().Update(roomInventory);
            }

            return roomInventory;
        }

        private EquipmentTransferEvent InsertTransferRequest(DateTime startDate, int sourceRoomId, int destinationRoomId, int inventoryItemId)
        {
            var transferEvent = UoW.GetRepository<IEquipmentTransferEventReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.InitialRoomId == sourceRoomId &&
                                     x.DestinationRoomId == destinationRoomId &&
                                     x.InventoryItemId == inventoryItemId);

            if (transferEvent == null)
            {
                transferEvent = new EquipmentTransferEvent()
                {
                    StartDate = startDate,
                    EndDate = new DateTime(2025, 11, 23, 16, 0, 0),
                    InitialRoomId = sourceRoomId,
                    DestinationRoomId = destinationRoomId,
                    InventoryItemId = inventoryItemId,
                    Quantity = 2
                };
                UoW.GetRepository<IEquipmentTransferEventWriteRepository>()
                    .Add(transferEvent);
            }

            return transferEvent;
        }
    }
}