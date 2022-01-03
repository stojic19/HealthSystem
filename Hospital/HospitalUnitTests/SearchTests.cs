using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using HospitalUnitTests.Base;
using Shouldly;
using System.Linq;
using Xunit;

namespace HospitalUnitTests
{
    public class SearchTests : BaseTest
    {
        public SearchTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Should_not_be_null()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test name",
                Description = "Test description",
                FloorNumber = 1,
                BuildingName = "Test building"
            });
            Context.SaveChanges();

            var rooms = UoW.GetRepository<IRoomReadRepository>().GetAll();
            rooms.ShouldNotBeNull();
        }

        [Fact]
        public void Should_get_two_Rooms()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test name",
                Description = "Test description",
                FloorNumber = 1,
                BuildingName = "Test building"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test name",
                Description = "Test description",
                FloorNumber = 1,
                BuildingName = "Test building"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 3,
                Name = "Room name",
                Description = "Test description",
                FloorNumber = 1,
                BuildingName = "Test building"
            });
            Context.SaveChanges();

            var rooms = UoW.GetRepository<IRoomReadRepository>().GetAll().
                Where(room => room.Name.ToLower().Contains("test"));
            rooms.ShouldNotBeNull();
            rooms.Count().ShouldBe(2);
        }

        [Fact]
        public void Should_get_one_item()
        {
            ClearDbContext();
            Room room = new Room()
            {
                Id = 1,
                Name = "Test name",
                Description = "Test description",
                FloorNumber = 1,
                BuildingName = "Test building"
            };
            InventoryItem inventoryItem = new InventoryItem()
            {
                Id = 1,
                Name = "Test item",
                InventoryItemType = 0
            };
            Context.RoomInventories.Add(new RoomInventory(1, room, inventoryItem, 3)) ;
            Context.SaveChanges();

            var roomInventories = UoW.GetRepository<IRoomInventoryReadRepository>().GetAll()
                .Where(ri => ri.InventoryItem.Name.ToLower().Contains("test item"));
            roomInventories.ShouldNotBeNull();
            roomInventories.Count().ShouldBe(1);
        }
    }
}
