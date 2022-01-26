using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.SharedModel.Model.Wrappers;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using System.Linq;
using Hospital.RoomsAndEquipment.Service;
using Xunit;
using Hospital.SharedModel.Model.Enumerations;
using Hospital.GraphicalEditor.Model;

namespace HospitalUnitTests
{
    public class TransferingEquipmentTests : BaseTest
    {
        public TransferingEquipmentTests(BaseFixture baseFixture) : base(baseFixture)
        {

        }
        
        [Fact]
        public void One_item_should_be_moved()
        {
            ClearDbContext();
            PrepareData();
           
            Context.EquipmentTransferEvents.Add(new EquipmentTransferEvent(1, new TimePeriod(new DateTime(2021, 11, 22, 0, 0, 0), new DateTime(2021, 11, 22, 16, 2, 2)),
               new RoomInventory(1, 1, 1, 3), new RoomInventory(2, 2, 1, 1), 1));

            Context.SaveChanges();

            var transferingService = new TransferingEquipmentService(UoW);
            transferingService.StartEquipmentTransferEvent();

            var initialRoomInventory = UoW.GetRepository<IRoomInventoryReadRepository>()
                .GetById(1);
            var destinationRoomInventory = UoW.GetRepository<IRoomInventoryReadRepository>()
               .GetById(2);

            initialRoomInventory.ShouldNotBeNull();
            initialRoomInventory.Amount.ShouldBe(2);
            destinationRoomInventory.ShouldNotBeNull();
            destinationRoomInventory.Amount.ShouldBe(2);
        }
        
           [Fact]
           public void Two_items_should_be_moved()
           {
            ClearDbContext();
            PrepareData();
            Context.EquipmentTransferEvents.Add(new EquipmentTransferEvent(1, new TimePeriod(new DateTime(2021, 11, 22, 0, 0, 0), new DateTime(2021, 11, 22, 16, 2, 2)),
                new RoomInventory(1, 1, 1, 2), new RoomInventory(2, 2, 1, 1), 2));
             
               Context.SaveChanges();

               var transferingService = new TransferingEquipmentService(UoW);
               transferingService.StartEquipmentTransferEvent();

               var initialRoomInventory = UoW.GetRepository<IRoomInventoryReadRepository>()
                   .GetById(1);
               var destinationRoomInventory = UoW.GetRepository<IRoomInventoryReadRepository>()
                  .GetById(2);

               initialRoomInventory.ShouldBeNull();
               destinationRoomInventory.ShouldNotBeNull();
               destinationRoomInventory.Amount.ShouldBe(3);
           }

           [Fact]
           public void Three_items_should_be_moved()
           {
            ClearDbContext();
            PrepareData();
            Context.EquipmentTransferEvents.Add(new EquipmentTransferEvent(1, new TimePeriod(new DateTime(2021, 11, 22, 0, 0, 0), new DateTime(2021, 11, 22, 16, 2, 2)),
                new RoomInventory(1, 1, 1, 3), new RoomInventory(2, 2, 1, 0), 3));
              
               Context.SaveChanges();

               var transferingService = new TransferingEquipmentService(UoW);
               transferingService.StartEquipmentTransferEvent();

               var initialRoomInventory = UoW.GetRepository<IRoomInventoryReadRepository>()
                   .GetById(1);
               var destinationRoomInventory = UoW.GetRepository<IRoomInventoryReadRepository>()
                  .GetById(2);

               initialRoomInventory.ShouldBeNull();
               destinationRoomInventory.ShouldNotBeNull();
               destinationRoomInventory.Amount.ShouldBe(3);
           }

        private void PrepareData() {

            Context.InventoryItems.Add(new InventoryItem()
            {
                Id = 1,
                Name = "Test item"
            });

            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room 1",
                Description = "Room description",
                BuildingName = "Building 1",
                FloorNumber = 1,
                Width = 5,
                Height = 6,
                RoomType = RoomType.AppointmentRoom,
                RoomPosition = new RoomPosition(0, 0, 150, 122)
            });

            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                Description = "Room description",
                BuildingName = "Building 1",
                FloorNumber = 1,
                Width = 5,
                Height = 6,
                RoomType = RoomType.AppointmentRoom,
                RoomPosition = new RoomPosition(0, 122, 150, 122)
            });

            Context.SaveChanges();
        }
    }
}
