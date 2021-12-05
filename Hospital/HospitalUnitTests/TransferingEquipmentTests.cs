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

namespace HospitalUnitTests
{
    public class TransferingEquipmentTests : BaseTest
    {
        public TransferingEquipmentTests(BaseFixture baseFixture) : base(baseFixture)
        {

        }

        [Fact]
        public void Count_should_be_twenty_four()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            { 
                Id = 2,
                Name = "Test room 2"
            });
            Context.SaveChanges();

            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
                .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(24);
        }

        [Fact]
        public void Count_should_be_twenty_three()
        {
            ClearDbContext();
            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1)
            };

            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 2,
                StartDate = DateTime.Now.AddHours(3),
                EndDate = DateTime.Now.AddHours(3.5),
                RoomId = 2,
            });
            Context.SaveChanges();

            var availableTerms = new TransferingEquipmentService(UoW)
                .GetAvailableTerms(timePeriod, 2, 1, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(23);
        }

        [Fact]
        public void Count_should_be_one()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(3),
                RoomId = 1
            });
            Context.SaveChanges();

            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(3)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
                .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(1);
        }

        [Fact]
        public void Should_be_empty()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = DateTime.Now.AddHours(6),
                EndDate = DateTime.Now.AddHours(8),
                RoomId = 1
            });
            Context.SaveChanges();

            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(6.5),
                EndTime = DateTime.Now.AddHours(8)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
                .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.ShouldBeEmpty();
        }

        [Fact]
        public void Two_terms_should_be_available()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = DateTime.Now.AddHours(3),
                EndDate = DateTime.Now.AddHours(3.5),
                RoomId = 1
            });
            Context.SaveChanges();

            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(5.5)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
               .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(2);
        }

        [Fact]
        public void One_term_should_be_available()
        {
            ClearDbContext();
            Context.Rooms.Add(new Room()
            {
                Id = 1,
                Name = "Test room"
            });
            Context.Rooms.Add(new Room()
            {
                Id = 2,
                Name = "Test room 2"
            });
            Context.ScheduledEvents.Add(new ScheduledEvent()
            {
                Id = 1,
                StartDate = DateTime.Now.AddHours(3),
                EndDate = DateTime.Now.AddHours(3.5),
                RoomId = 1
            });
            Context.SaveChanges();
            var timePeriod = new TimePeriod()
            {
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(3.5)
            };

            var availableTerms = new TransferingEquipmentService(UoW)
               .GetAvailableTerms(timePeriod, 1, 2, 1);
            availableTerms.ShouldNotBeNull();
            availableTerms.Count().ShouldBe(1);
        }

        [Fact]
        public void One_item_should_be_moved()
        {
            ClearDbContext();

            Context.EquipmentTransferEvents.Add(new EquipmentTransferEvent()
            {
                Id = 1,
                StartDate = new DateTime(2021, 11, 22, 0, 0, 0),
                EndDate = new DateTime(2021, 11, 22, 16, 2, 2),
                InitalRoom = new Room() {
                    Id = 1,
                    Name = "Test initial room"
                },
                DestinationRoom = new Room()
                {
                    Id = 2,
                    Name = "Test destination room"
                },
                Quantity = 1,
                InventoryItem = new InventoryItem()
                {
                    Id = 1,
                    Name = "Test item"
                }

            });
            Context.RoomInventories.Add(new RoomInventory()
            {
                Id = 1,
                InventoryItemId = 1,
                Amount = 3,
                RoomId = 1
            });
            Context.RoomInventories.Add(new RoomInventory()
            {
                Id = 2,
                InventoryItemId = 1,
                Amount = 1,
                RoomId = 2
            });
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

            Context.EquipmentTransferEvents.Add(new EquipmentTransferEvent()
            {
                Id = 1,
                StartDate = new DateTime(2021, 11, 22, 0, 0, 0),
                EndDate = new DateTime(2021, 11, 22, 16, 2, 2),
                InitalRoom = new Room()
                {
                    Id = 1,
                    Name = "Test initial room"
                },
                DestinationRoom = new Room()
                {
                    Id = 2,
                    Name = "Test destination room"
                },
                Quantity = 2,
                InventoryItem = new InventoryItem()
                {
                    Id = 1,
                    Name = "Test item"
                }

            });
            Context.RoomInventories.Add(new RoomInventory()
            {
                Id = 1,
                InventoryItemId = 1,
                Amount = 2,
                RoomId = 1
            });
            Context.RoomInventories.Add(new RoomInventory()
            {
                Id = 2,
                InventoryItemId = 1,
                Amount = 1,
                RoomId = 2
            });
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
            Context.EquipmentTransferEvents.Add(new EquipmentTransferEvent()
            {
                Id = 1,
                StartDate = new DateTime(2021, 11, 22, 0, 0, 0),
                EndDate = new DateTime(2021, 11, 22, 16, 2, 2),
                InitalRoom = new Room()
                {
                    Id = 1,
                    Name = "Test initial room"
                },
                DestinationRoom = new Room()
                {
                    Id = 2,
                    Name = "Test destination room"
                },
                Quantity = 3,
                InventoryItem = new InventoryItem()
                {
                    Id = 1,
                    Name = "Test item"
                }

            });
            Context.RoomInventories.Add(new RoomInventory()
            {
                Id = 1,
                InventoryItemId = 1,
                Amount = 3,
                RoomId = 1
            });
            Context.RoomInventories.Add(new RoomInventory()
            {
                Id = 2,
                InventoryItem = new InventoryItem()
                {
                    Id = 2,
                    Name = "Test item",
                },
                Amount = 3,
                RoomId = 1
            });
            Context.SaveChanges();

            var transferingService = new TransferingEquipmentService(UoW);
            transferingService.StartEquipmentTransferEvent();

            var initialRoomInventory = UoW.GetRepository<IRoomInventoryReadRepository>()
                .GetById(1);
            var destinationRoomInventory = UoW.GetRepository<IRoomInventoryReadRepository>()
               .GetById(3);

            initialRoomInventory.ShouldBeNull();
            destinationRoomInventory.ShouldNotBeNull();
            destinationRoomInventory.Amount.ShouldBe(3);
        }
    }
}
