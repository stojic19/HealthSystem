using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.SharedModel.Model.Wrappers;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using Xunit;

namespace HospitalUnitTests
{
    public class CancellingEventTests : BaseTest
    {
        private readonly CancellingEventsService _cancellingEventsService;
        public CancellingEventTests(BaseFixture fixture) : base(fixture)
        {
            _cancellingEventsService = new(UoW);
        }

        [Fact]
        public void Transfer_event_should_be_cancelled()
        {
            #region Arrange
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test name",
                FloorNumber = 1,
                BuildingName = "Test building"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                FloorNumber = 1,
                BuildingName = "Test building"
            });

            Context.InventoryItems.Add(new InventoryItem()
            {
                Id = 1,
                InventoryItemType = InventoryItemType.Dynamic,
                Name = "Test item"
            });
            var transferEvent = new EquipmentTransferEvent(1, new TimePeriod(new DateTime(2022, 12, 22, 12, 0, 0), new DateTime(2022, 12, 22, 13, 0, 0)),
                new RoomInventory(1, 1, 1, 3), new RoomInventory(2, 2, 1, 0), 2);
          
            Context.EquipmentTransferEvents.Add(transferEvent);
            Context.SaveChanges();
            #endregion
            _cancellingEventsService.CancelEquipmentTransferEvent(transferEvent);

            var transferEvents = UoW.GetRepository<IEquipmentTransferEventReadRepository>()
                .GetById(transferEvent.Id);
            transferEvents.ShouldBeNull();
        }

        [Fact]
        public void Renovation_should_not_be_cancelled()
        {
            #region Arrange

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
            #endregion
            /**
             * Instead of testing private methods directly, test them indirectly as part of the overarching observable behavior
             * DOMENSKI ZNACAJNA I ISPITUJE PRIVATNU METODU
             */
            _cancellingEventsService.CancelRoomRenovationEvent(renovation);

            var updatedEvent = UoW.GetRepository<IRoomRenovationEventReadRepository>()
                .GetById(renovation.Id);
            updatedEvent.ShouldBeNull();
        }
    }
}
