using Hospital.Model;
using Hospital.Model.Enumerations;
using Hospital.Repositories;
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
    public class EquipmentTransferControllerTest : BaseTest
    {
        public EquipmentTransferControllerTest(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Add_transfer_request_should_return_200()
        {
            InsertRoom(2);
            InsertRoom(3);
            InsertInventoryItem(11);

            CheckAndDeleteRequests(new DateTime(2025, 11, 22, 0, 0, 0));

            var newRequest = new EquipmentTransferEvent()
            {
                StartDate = new DateTime(2025, 11, 22, 0, 0, 0),
                EndDate = new DateTime(2025, 11, 22, 16, 2, 2),
                InitalRoomId = 2,
                DestinationRoomId = 3,
                InventoryItemId = 11,
                Quantity = 2
            };

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/EquipmentTransferEvent/addEvent", content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.ShouldNotBeNull();

            var foundRequest = UoW.GetRepository<IEquipmentTransferEventReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.StartDate == newRequest.StartDate &&
                                x.EndDate == newRequest.EndDate &&
                                x.InitalRoomId == newRequest.InitalRoomId &&
                                x.DestinationRoomId == newRequest.DestinationRoomId &&
                                x.InventoryItemId == newRequest.InventoryItemId);

            foundRequest.ShouldNotBeNull();
        }

        [Fact]
        public async Task Add_transfer_request_should_return_400()
        {
            InsertRoom(2);
            InsertRoom(3);
            InsertInventoryItem(11);

            CheckAndDeleteRequests(new DateTime(2025, 11, 22, 0, 0, 0));

            var newRequest = new EquipmentTransferEvent()
            {
                StartDate = new DateTime(2025, 11, 22, 0, 0, 0),
                EndDate = new DateTime(2025, 11, 22, 16, 2, 2),
                InitalRoomId = 2,
                DestinationRoomId = 3,
                InventoryItemId = 11,
                Quantity = 58
            };

            var content = GetContent(newRequest);

            var response = await Client.PostAsync(BaseUrl + "api/EquipmentTransferEvent/addEvent", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            response.ShouldNotBeNull();

            var foundRequest = UoW.GetRepository<IEquipmentTransferEventReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.StartDate == newRequest.StartDate &&
                                x.EndDate == newRequest.EndDate &&
                                x.InitalRoomId == newRequest.InitalRoomId &&
                                x.DestinationRoomId == newRequest.DestinationRoomId &&
                                x.InventoryItemId == newRequest.InventoryItemId);

            foundRequest.ShouldBeNull();
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

        private void InsertInventoryItem(int inventoryId)
        {
            var inventoryItem = UoW.GetRepository<IInventoryItemReadRepository>()
                .GetById(inventoryId);

            if(inventoryItem == null)
            {
                inventoryItem = new InventoryItem()
                {
                    Name = "Chair",
                    InventoryItemType = InventoryItemType.Dynamic
                };

                UoW.GetRepository<IInventoryItemWriteRepository>().Add(inventoryItem);
            }
        }

        private void InsertRoom(int roomId)
        {
            var room = UoW.GetRepository<IRoomReadRepository>()
                .GetById(roomId);

            if (room == null)
            {
                room = new Room()
                {
                    Name = "SR-2",
                    Description = "Room for storage",
                    DimensionX = 7,
                    DimensionY = 8.5,
                    FloorNumber = 1,
                    BuildingName = "Building 2",
                    RoomType = RoomType.Storage
                };

                UoW.GetRepository<IRoomWriteRepository>().Add(room);
            }
        }
    }
}
