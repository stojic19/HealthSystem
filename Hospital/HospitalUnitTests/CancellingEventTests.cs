using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.SharedModel.Model.Enumerations;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalUnitTests
{
    public class CancellingEventTests : BaseTest
    {
        public CancellingEventTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Transfer_event_should_be_cancelled()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test name",
                FloorNumber = 1,
                BuildingName = "Test building"
            });
            var transferEvent = new EquipmentTransferEvent()
            {
                Id = 1,
                StartDate = new DateTime(2022, 12, 22, 12, 0, 0),
                EndDate = new DateTime(2022, 12, 22, 13, 0, 0),
                InitialRoomId = 1,
                DestinationRoom = new Room()
                {
                    Id = 2,
                    Name = "Test room 2",
                    FloorNumber = 1,
                    BuildingName = "Test building"
                },
                InventoryItem = new InventoryItem()
                {
                    Id = 1,
                    InventoryItemType = InventoryItemType.Dynamic,
                    Name = "Test item"
                },
                Quantity = 2,
            };
            Context.EquipmentTransferEvents.Add(transferEvent);
            Context.SaveChanges();

            var cancellingService = new CancellingEventsService(UoW);
            cancellingService.CancelEquipmentTransferEvent(transferEvent);

            var updatedEvent = UoW.GetRepository<IEquipmentTransferEventReadRepository>()
                .GetById(transferEvent.Id);
            updatedEvent.ShouldBeNull();
        }

        [Fact]
        public void Renovation_should_not_be_cancelled()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room name",
                FloorNumber = 1,
                BuildingName = "Test building"
            });
            var renovation = new RoomRenovationEvent()
            {
                Id = 1,
                StartDate = new DateTime(2022, 12, 25, 12, 0, 0),
                EndDate = new DateTime(2022, 12, 26, 13, 0, 0),
                IsMerge = false,
                RoomId = 1
            };
            Context.RoomRenovationEvents.Add(renovation);
            Context.SaveChanges();

            var cancellingService = new CancellingEventsService(UoW);
            cancellingService.CancelRoomRenovationEvent(renovation);

            var updatedEvent = UoW.GetRepository<IRoomRenovationEventReadRepository>()
                .GetById(renovation.Id);
            updatedEvent.ShouldBeNull();
        }
    }
}
