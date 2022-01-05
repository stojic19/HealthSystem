using Hospital.GraphicalEditor.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Model.Enumerations;
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
    public class RoomRenovationControllerTests : BaseTest
    {
        public RoomRenovationControllerTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Add_renovation_request_should_return_200()
        {
            var room = InsertRoom("Test room 1");
            var surroundingRoom = InsertSurroundingRoom("Test surrounding room");

            CheckAndDeleteRequests(new DateTime(2025, 11, 22, 0, 0, 0));

            var newRequest = new RoomRenovationEvent()
            {
                StartDate = new DateTime(2025, 11, 22, 0, 0, 0),
                EndDate = new DateTime(2025, 11, 22, 16, 2, 2),
                RoomId = room.Id,
                IsMerge = true,
                MergeRoomId = surroundingRoom.Id,
                FirstRoomName = "AR-12m",
                FirstRoomDescription = "Room for appointments",
                FirstRoomType = RoomType.AppointmentRoom,
                SecondRoomName = "",
                SecondRoomDescription = "",
                SecondRoomType = RoomType.AppointmentRoom
            };

            var content = GetContent(newRequest);
            var response = await Client.PostAsync(BaseUrl + "api/RoomRenovation/AddNewRoomRenovationEvent", content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.ShouldNotBeNull();

            var foundRequest = UoW.GetRepository<IRoomRenovationEventReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.StartDate == newRequest.StartDate &&
                                x.EndDate == newRequest.EndDate &&
                                x.RoomId == newRequest.RoomId &&
                                x.IsMerge == newRequest.IsMerge &&
                                x.MergeRoomId == newRequest.MergeRoomId &&
                                x.FirstRoomName == newRequest.FirstRoomName &&
                                x.FirstRoomDescription == newRequest.FirstRoomDescription &&
                                x.FirstRoomType == newRequest.FirstRoomType &&
                                x.SecondRoomName == newRequest.SecondRoomName &&
                                x.SecondRoomDescription == newRequest.SecondRoomDescription &&
                                x.SecondRoomType == newRequest.SecondRoomType);

            foundRequest.ShouldNotBeNull();
            ClearAllTestData();
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
                    Description = "Room for appointments",
                    FloorNumber = 1,
                    Width = 5.0,
                    Height = 4.5,
                    BuildingName = "Building 1",
                    RoomType = RoomType.AppointmentRoom
                };

                UoW.GetRepository<IRoomWriteRepository>().Add(room);
            }

            return room;
        }

        private Room InsertSurroundingRoom(string name)
        {
            var room = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == name);

            if (room == null)
            {
                room = new Room()
                {
                    Name = name,
                    Description = "Room for appointments",
                    FloorNumber = 1,
                    Width = 4.0,
                    Height = 3.5,
                    BuildingName = "Building 1",
                    RoomType = RoomType.AppointmentRoom
                };

                UoW.GetRepository<IRoomWriteRepository>().Add(room);
            }

            return room;
        }

        private void CheckAndDeleteRequests(DateTime startDate)
        {
            var requests = UoW.GetRepository<IRoomRenovationEventReadRepository>()
                .GetAll()
                .Where(x => x.StartDate == startDate);

            if (requests.Any())
            {
                UoW.GetRepository<IRoomRenovationEventWriteRepository>().DeleteRange(requests);
            }
        }

        private void ClearAllTestData()
        {
            var initialRoom = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == "Test room 1");
            var secondRoom = UoW.GetRepository<IRoomReadRepository>()
                .GetAll()
                .FirstOrDefault(x => x.Name == "Test surrounding room");

            if (initialRoom != null)
                UoW.GetRepository<IRoomWriteRepository>().Delete(initialRoom);

            if (secondRoom != null)
                UoW.GetRepository<IRoomWriteRepository>().Delete(secondRoom);

        }
    }
}
