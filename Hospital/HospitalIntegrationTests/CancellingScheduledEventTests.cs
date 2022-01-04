using AutoMapper;
using Hospital.GraphicalEditor.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Model.Enumerations;
using HospitalApi.DTOs;
using HospitalIntegrationTests.Base;
using Microsoft.EntityFrameworkCore;
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
            var sourceRoom = InsertRoom("Test initial room");
            var destinationRoom = InsertRoom("Test destination room");
            var inventoryItem = InsertInventoryItem("Test item");
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

        [Fact]
        public async Task Renovation_should_be_canceled()
        {
            var room = InsertRoom("Test renovating room");
            var roomRenovation = InsertRoomRenovationEvent(new DateTime(2025, 11, 22, 0, 0, 0), new DateTime(2025, 11, 25, 0, 0, 0), room.Id, false);

            var content = GetContent(new RoomRenovationEventDto()
            {
                Id = roomRenovation.Id,
                StartDate = roomRenovation.StartDate,
                EndDate = roomRenovation.EndDate,
                RoomId = roomRenovation.RoomId,
                IsMerge = roomRenovation.IsMerge,
                MergeRoomId = roomRenovation.MergeRoomId
            });

            var response = await Client.PostAsync(BaseUrl + "api/RoomRenovation/CancelRenovation", content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.ShouldNotBeNull();

            var canceledRequest = UoW.GetRepository<IRoomRenovationEventReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.StartDate == roomRenovation.StartDate &&
                                     x.EndDate == roomRenovation.EndDate &&
                                     x.RoomId == roomRenovation.RoomId &&
                                     x.IsMerge == roomRenovation.IsMerge);

            canceledRequest.ShouldBeNull();
        }

        public RoomRenovationEvent InsertRoomRenovationEvent(DateTime startDate, DateTime endDate, int roomId, bool isMerge)
        {
            var renovation = UoW.GetRepository<IRoomRenovationEventReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.StartDate == startDate &&
                                     x.EndDate == endDate &&
                                     x.RoomId == roomId && 
                                     x.IsMerge == isMerge);

            if (renovation == null)
            {
                renovation = new RoomRenovationEvent()
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    RoomId = roomId,
                    IsMerge = isMerge,
                    FirstRoomName = "Test renovation room name",
                    FirstRoomDescription = "Test renovation room description",
                    FirstRoomType = RoomType.AppointmentRoom
                };

                UoW.GetRepository<IRoomRenovationEventWriteRepository>().Add(renovation);
            }

            return renovation;
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
                    Description = "Test room",
                    Width = 7,
                    Height = 8.5,
                    FloorNumber = 1,
                    BuildingName = "Building 2",
                    RoomType = RoomType.Storage,
                    RoomPosition = new RoomPosition(0, 0, 150, 122)
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
                roomInventory = new RoomInventory(roomId, inventoryId, amount);
                UoW.GetRepository<IRoomInventoryWriteRepository>().Add(roomInventory);
            }
            else if (roomInventory.Amount < amount)
            {
                roomInventory.Add(amount - roomInventory.Amount);
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

        private void ClearAllTestData()
        {
            var initialRoom = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == "Test initial room");
            var destinationRoom = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == "Test destination room");
            var inventoryItem = UoW.GetRepository<IInventoryItemReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == "Test item");
            var roomInventory = UoW.GetRepository<IRoomInventoryReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.InventoryItemId == inventoryItem.Id &&
                                     x.RoomId == initialRoom.Id);

            if (roomInventory != null)
                UoW.GetRepository<IRoomInventoryWriteRepository>().Delete(roomInventory);

            if (inventoryItem != null)
                UoW.GetRepository<IInventoryItemWriteRepository>().Delete(inventoryItem);

            if (initialRoom != null)
                UoW.GetRepository<IRoomWriteRepository>().Delete(initialRoom);

            if (destinationRoom != null)
                UoW.GetRepository<IRoomWriteRepository>().Delete(destinationRoom);
        }

    }
}