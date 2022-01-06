using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital.RoomsAndEquipment.Model;
using HospitalUnitTests.Base;
using System.Linq;
using Xunit;
using Shouldly;
using Hospital.RoomsAndEquipment.Repository;

namespace HospitalUnitTests
{
    public class SurroundingRoomsTests : BaseTest
    {
        public SurroundingRoomsTests(BaseFixture baseFixture) : base(baseFixture)
        {

        }

        [Fact]
        public void Rooms_should_be_neighbors()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room 1",
                BuildingName = "Building 1",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(0, 0, 150, 122)
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                BuildingName = "Building 1",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(0, 122, 150, 122)
            });
            
            Context.SaveChanges();

            var firstRoom = UoW.GetRepository<IRoomReadRepository>().GetById(1);
            var secondRoom = UoW.GetRepository<IRoomReadRepository>().GetById(2);
            var result = firstRoom.AreNeighbors(secondRoom);

            result.ShouldBeTrue();
        }

        [Fact]
        public void Rooms_should_not_be_neighbors()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room 1",
                BuildingName = "Building 1",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(0, 0, 150, 122)
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                BuildingName = "Building 1",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(0, 366, 150, 122)

            });
            
            Context.SaveChanges();

            var firstRoom = UoW.GetRepository<IRoomReadRepository>().GetById(1);
            var secondRoom = UoW.GetRepository<IRoomReadRepository>().GetById(2);
            var result = firstRoom.AreNeighbors(secondRoom);

            result.ShouldBeFalse();
        }

        [Fact]
        public void Rooms_should_be_neighbors_again()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room 1",
                BuildingName = "Building 2",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(0, 0, 225, 130)
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                BuildingName = "Building 2",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(225, 0, 225, 130)
            });
      
            Context.SaveChanges();

            var firstRoom = UoW.GetRepository<IRoomReadRepository>().GetById(1);
            var secondRoom = UoW.GetRepository<IRoomReadRepository>().GetById(2);
            var result = firstRoom.AreNeighbors(secondRoom);

            result.ShouldBeTrue();
        }

        [Fact]
        public void Should_find_two_neighbors()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room 1",
                BuildingName = "Building 2",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(0, 0, 225, 130)
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                BuildingName = "Building 2",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(225, 0, 225, 130)
            });
            Context.Rooms.Add(new Room()
            {
                Id = 3,
                Name = "Test room 3",
                BuildingName = "Building 2",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(450, 0, 225, 130)
            });
          
            Context.SaveChanges();

            var service = new FindingSurroundingRoomsService(UoW);
            var room= UoW.GetRepository<IRoomReadRepository>().GetById(2);
            var result = service.GetSurroundingRooms(room);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
        }

        [Fact]
        public void Should_find_one_neighbor()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room 1",
                BuildingName = "Building 1",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(0, 0, 150, 122)
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                BuildingName = "Building 1",
                FloorNumber = 1,
                RoomPosition = new RoomPosition(0, 122, 150, 122)
            });
         
            Context.SaveChanges();

            var service = new FindingSurroundingRoomsService(UoW);
            var room = UoW.GetRepository<IRoomReadRepository>().GetById(2);
            var result = service.GetSurroundingRooms(room);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(1);
        }
    }
}
