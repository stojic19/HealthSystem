using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital.GraphicalEditor.Repository;
using Hospital.RoomsAndEquipment.Model;
using HospitalUnitTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

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
                FloorNumber = 1
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                BuildingName = "Building 1",
                FloorNumber = 1
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 1,
                RoomId = 1,
                DimensionY = 0,
                DimensionX = 0,
                Width = 150,
                Height = 122
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 2,
                RoomId = 2,
                DimensionY = 122,
                DimensionX = 0,
                Width = 150,
                Height = 122
            });
            Context.SaveChanges();

            var service = new FindingSurroundingRoomsService(UoW);
            var firstPosition = UoW.GetRepository<IRoomPositionReadRepository>().GetById(1);
            var secondPosition = UoW.GetRepository<IRoomPositionReadRepository>().GetById(2);
            var result = service.AreNeighbors(firstPosition, secondPosition);

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
                FloorNumber = 1
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                BuildingName = "Building 1",
                FloorNumber = 1
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 1,
                RoomId = 1,
                DimensionY = 0,
                DimensionX = 0,
                Width = 150,
                Height = 122
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 2,
                RoomId = 2,
                DimensionY = 366,
                DimensionX = 0,
                Width = 150,
                Height = 122
            });
            Context.SaveChanges();

            var service = new FindingSurroundingRoomsService(UoW);
            var firstPosition = UoW.GetRepository<IRoomPositionReadRepository>().GetById(1);
            var secondPosition = UoW.GetRepository<IRoomPositionReadRepository>().GetById(2);
            var result = service.AreNeighbors(firstPosition, secondPosition);

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
                FloorNumber = 1
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                BuildingName = "Building 2",
                FloorNumber = 1
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 1,
                RoomId = 1,
                DimensionY = 0,
                DimensionX = 0,
                Width = 225,
                Height = 130
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 2,
                RoomId = 2,
                DimensionY = 0,
                DimensionX = 225,
                Width = 225,
                Height = 130
            });
            Context.SaveChanges();

            var service = new FindingSurroundingRoomsService(UoW);
            var firstPosition = UoW.GetRepository<IRoomPositionReadRepository>().GetById(1);
            var secondPosition = UoW.GetRepository<IRoomPositionReadRepository>().GetById(2);
            var result = service.AreNeighbors(firstPosition, secondPosition);

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
                FloorNumber = 1
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                BuildingName = "Building 2",
                FloorNumber = 1
            });
            Context.Rooms.Add(new Room()
            {
                Id = 3,
                Name = "Test room 3",
                BuildingName = "Building 2",
                FloorNumber = 1
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 1,
                RoomId = 1,
                DimensionY = 0,
                DimensionX = 0,
                Width = 225,
                Height = 130
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 2,
                RoomId = 2,
                DimensionY = 0,
                DimensionX = 225,
                Width = 225,
                Height = 130
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 3,
                RoomId = 3,
                DimensionY = 0,
                DimensionX = 450,
                Width = 225,
                Height = 130
            });
            Context.SaveChanges();

            var service = new FindingSurroundingRoomsService(UoW);
            var firstPosition = UoW.GetRepository<IRoomPositionReadRepository>().GetById(2);
            var result = service.GetSurroundingRooms(firstPosition);
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
                FloorNumber = 1
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2",
                BuildingName = "Building 1",
                FloorNumber = 1
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 1,
                RoomId = 1,
                DimensionY = 0,
                DimensionX = 0,
                Width = 150,
                Height = 122
            });
            Context.RoomPositions.Add(new RoomPosition()
            {
                Id = 2,
                RoomId = 2,
                DimensionY = 122,
                DimensionX = 0,
                Width = 150,
                Height = 122
            });
            Context.SaveChanges();

            var service = new FindingSurroundingRoomsService(UoW);
            var firstPosition = UoW.GetRepository<IRoomPositionReadRepository>().GetById(2);
            var result = service.GetSurroundingRooms(firstPosition);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(1);
        }
    }
}
