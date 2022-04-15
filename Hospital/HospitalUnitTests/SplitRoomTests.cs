using Hospital.GraphicalEditor.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.SharedModel.Model.Enumerations;
using HospitalUnitTests.Base;
using Shouldly;
using System.Linq;
using Xunit;

namespace HospitalUnitTests
{
    public class SplitRoomTests : BaseTest
    {
        private readonly RenovatingRoomsService _renovatingRoomsService;
        public SplitRoomTests(BaseFixture baseFixture) : base(baseFixture)
        {
            _renovatingRoomsService = new(UoW);
        }

        [Fact]
        public void Two_new_rooms_should_be_created()
        {
            PrepareData();

            var roomRenovationEvent = UoW.GetRepository<IRoomRenovationEventReadRepository>().GetById(1);
            _renovatingRoomsService.SplitRoom(roomRenovationEvent);
            var rooms = UoW.GetRepository<IRoomReadRepository>().GetAll();
            rooms.Count().ShouldBe(2);
        }

        [Fact]
        public void Room_sizes_should_change()
        {
            PrepareData();

            var oldRoom = UoW.GetRepository<IRoomReadRepository>().GetById(1);
            oldRoom.Width.ShouldBe(5);
            oldRoom.Height.ShouldBe(6);

            var roomRenovationEvent = UoW.GetRepository<IRoomRenovationEventReadRepository>().GetById(1);
            _renovatingRoomsService.SplitRoom(roomRenovationEvent);
            var firstRoom = UoW.GetRepository<IRoomReadRepository>().GetById(1);
            firstRoom.Width.ShouldBe(5);
            firstRoom.Height.ShouldBe(3);
            var secondRoom = UoW.GetRepository<IRoomReadRepository>().GetAll().Where(room => room.Name == "New Room").FirstOrDefault();
            secondRoom.Width.ShouldBe(5);
            secondRoom.Height.ShouldBe(3);

        }

        private void PrepareData()
        {

            ClearDbContext();

            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room",
                Description = "Room description",
                BuildingName = "Building 1",
                FloorNumber = 1,
                Width = 5,
                Height = 6,
                RoomType = RoomType.AppointmentRoom,
                RoomPosition = new RoomPosition(0, 0, 150, 122)
            });

            Context.RoomRenovationEvents.Add(new RoomRenovationEvent()
            {
                Id = 1,
                RoomId = 1,
                SecondRoomName = "New Room"
            }); 

            Context.SaveChanges();
        }

    }
}
