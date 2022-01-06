using Hospital.GraphicalEditor.Model;
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
    public class MergeRoomsTests : BaseTest
    {
        public MergeRoomsTests(BaseFixture baseFixture) : base(baseFixture)
        {

        }

        [Fact]
        public void Two_rooms_should_be_merged()
        {
            PrepareData();
            var service = new RenovatingRoomsService(UoW);
            var roomRenovationEvent = UoW.GetRepository<IRoomRenovationEventReadRepository>().GetById(1);
            service.MergeRooms(roomRenovationEvent);
            var rooms = UoW.GetRepository<IRoomReadRepository>().GetAll();
            rooms.Count().ShouldBe(1);
        }

        [Fact]
        public void Inventory_should_be_moved()
        {
            PrepareData();
            var service = new RenovatingRoomsService(UoW);
            var roomRenovationEvent = UoW.GetRepository<IRoomRenovationEventReadRepository>().GetById(1);
            service.MergeRooms(roomRenovationEvent);
            var roomInventory = UoW.GetRepository<IRoomInventoryReadRepository>().GetById(2);
            roomInventory.RoomId.ShouldBe(1);
            var roomInventoryNew = UoW.GetRepository<IRoomInventoryReadRepository>().GetById(1);
            roomInventoryNew.Amount.ShouldBe(4);
        }

        [Fact]
        public void Room_size_should_change()
        {
            PrepareData();
            var firstRoom = UoW.GetRepository<IRoomReadRepository>().GetById(1);
            firstRoom.Width.ShouldBe(5);
            firstRoom.Height.ShouldBe(6);
            var secondRoom = UoW.GetRepository<IRoomReadRepository>().GetById(2);
            secondRoom.Width.ShouldBe(5);
            secondRoom.Height.ShouldBe(6);
            var service = new RenovatingRoomsService(UoW);
            var roomRenovationEvent = UoW.GetRepository<IRoomRenovationEventReadRepository>().GetById(1);
            service.MergeRooms(roomRenovationEvent);
            var newRoom = UoW.GetRepository<IRoomReadRepository>().GetById(1);
            newRoom.Width.ShouldBe(5);
            newRoom.Height.ShouldBe(12);
        }



        private void PrepareData() {

            ClearDbContext();

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

            Context.RoomRenovationEvents.Add(new RoomRenovationEvent()
            {
                Id = 1,
                RoomId = 1,
                IsMerge = true,
                MergeRoomId = 2
            });

            Context.InventoryItems.Add(new InventoryItem()
            {
                Id = 1,
                Name = "Test item 1"
            });

            Context.InventoryItems.Add(new InventoryItem()
            {
                Id = 2,
                Name = "Test item 2"
            });

            Context.RoomInventories.Add(new RoomInventory(1, 1, 1, 2));

            Context.RoomInventories.Add(new RoomInventory(2, 2, 2, 2));

            Context.RoomInventories.Add(new RoomInventory(3, 2, 1, 2));

            Context.SaveChanges();
        }
    }
}
