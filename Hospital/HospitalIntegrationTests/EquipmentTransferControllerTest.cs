using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Model.Enumerations;
using HospitalIntegrationTests.Base;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HospitalIntegrationTests
{
    public class EquipmentTransferControllerTest : BaseTest
    {
        public EquipmentTransferControllerTest(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Add_transfer_request_should_return_200()
        {
            var sourceRoom = InsertRoom("Test initial room");
            var destinationRoom = InsertRoom("Test destination room");
            var inventoryItem = InsertInventoryItem("Test item");
            var roomInventoryItem = InsertInventoryInRoom(sourceRoom.Id, inventoryItem.Id, 4);

            CheckAndDeleteRequests(new DateTime(2025, 11, 22, 0, 0, 0));

            var newRequest = new EquipmentTransferEvent()
            {
                StartDate = new DateTime(2025, 11, 22, 0, 0, 0),
                EndDate = new DateTime(2025, 11, 22, 16, 2, 2),
                InitialRoomId = sourceRoom.Id,
                DestinationRoomId = destinationRoom.Id,
                InventoryItemId = inventoryItem.Id,
                Quantity = 2
            };

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/EquipmentTransferEvent/AddNewEquipmentTransferEvent", content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.ShouldNotBeNull();

            var foundRequest = UoW.GetRepository<IEquipmentTransferEventReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.StartDate == newRequest.StartDate &&
                                x.EndDate == newRequest.EndDate &&
                                x.InitialRoomId == newRequest.InitialRoomId &&
                                x.DestinationRoomId == newRequest.DestinationRoomId &&
                                x.InventoryItemId == newRequest.InventoryItemId);

            foundRequest.ShouldNotBeNull();
            ClearAllTestData();
        }

        [Fact]
        public async Task Add_transfer_request_should_return_400()
        {
            var sourceRoom = InsertRoom("Test initial room");
            var destinationRoom = InsertRoom("Test destination room");
            var inventoryItem = InsertInventoryItem("Test item");
            var roomInventoryItem = InsertInventoryInRoom(sourceRoom.Id, inventoryItem.Id, 4);

            CheckAndDeleteRequests(new DateTime(2025, 11, 22, 0, 0, 0));

            var newRequest = new EquipmentTransferEvent()
            {
                StartDate = new DateTime(2025, 11, 22, 0, 0, 0),
                EndDate = new DateTime(2025, 11, 22, 16, 2, 2),
                InitialRoomId = sourceRoom.Id,
                DestinationRoomId = destinationRoom.Id,
                InventoryItemId = inventoryItem.Id,
                Quantity = 58
            };

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/EquipmentTransferEvent/AddNewEquipmentTransferEvent", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            response.ShouldNotBeNull();

            var foundRequest = UoW.GetRepository<IEquipmentTransferEventReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.StartDate == newRequest.StartDate &&
                                x.EndDate == newRequest.EndDate &&
                                x.InitialRoomId == newRequest.InitialRoomId &&
                                x.DestinationRoomId == newRequest.DestinationRoomId &&
                                x.InventoryItemId == newRequest.InventoryItemId);

            foundRequest.ShouldBeNull();
            ClearAllTestData();
        }

        private void CheckAndDeleteRequests(DateTime startDate)
        {
            var requests = UoW.GetRepository<IEquipmentTransferEventReadRepository>()
                .GetAll()
                .Where(x => x.StartDate == startDate);

            if (requests.Any())
            {
                UoW.GetRepository<IEquipmentTransferEventWriteRepository>().DeleteRange(requests);
            }
        }

        private InventoryItem InsertInventoryItem(string name)
        {
            var inventoryItem = UoW.GetRepository<IInventoryItemReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == name);

            if(inventoryItem == null)
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
                    RoomType = RoomType.Storage
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
