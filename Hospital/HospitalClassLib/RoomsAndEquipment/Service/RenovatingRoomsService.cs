using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.RoomsAndEquipment.Service
{
    public class RenovatingRoomsService
    {
        private readonly IUnitOfWork uow;

        public RenovatingRoomsService(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        public void StartRoomRenovations()
        {
            var repo = uow.GetRepository<IRoomRenovationEventReadRepository>();
            foreach (RoomRenovationEvent roomRenovationEvent in repo.GetAll().ToList())
            {
                if (DateTime.Compare(roomRenovationEvent.EndDate, DateTime.Now) <= 0)
                {
                    if (roomRenovationEvent.IsMerge)
                        MergeRooms(roomRenovationEvent);
                    else
                        SplitRoom(roomRenovationEvent);
                }
            }
        }

        public void SplitRoom(RoomRenovationEvent roomRenovationEvent)
        {
            Room selectedRoom = roomRenovationEvent.Room;
            double width;
            double height;
            // kreiranje nove sobe
            if (selectedRoom.BuildingName == "Building 1")
            {
                width = selectedRoom.Width;
                height = selectedRoom.Height / 2;
                selectedRoom.Height = height;
            }
            else
            {
                width = selectedRoom.Width / 2;
                height = selectedRoom.Height;
                selectedRoom.Width = width;
            }

            Room newRoom = new Room()
            {
                Name = selectedRoom.Name + "s",
                Description = selectedRoom.Description,
                BuildingName = selectedRoom.BuildingName,
                FloorNumber = selectedRoom.FloorNumber,
                RoomType = selectedRoom.RoomType,
                Width = width,
                Height = height
            };

            var roomWriteRepo = uow.GetRepository<IRoomWriteRepository>();
            roomWriteRepo.Add(newRoom);
            roomWriteRepo.Update(selectedRoom);
            // kreiranje novih pozicija
            GetNewPositionsWhenSplitting(selectedRoom, newRoom);
        }

        private void GetNewPositionsWhenSplitting(Room selectedRoom, Room newRoom)
        {
            var roomPositionsWriteRepo = uow.GetRepository<IRoomPositionWriteRepository>();
            var roomPositionReadRepo = uow.GetRepository<IRoomPositionReadRepository>();
            var oldRoomPosition = roomPositionReadRepo.GetByRoom(selectedRoom.Id);
            RoomPosition newRoomPosition;

            if (selectedRoom.BuildingName == "Building 1")
            {
                newRoomPosition = new RoomPosition()
                {
                    DimensionX = oldRoomPosition.DimensionX,
                    DimensionY = oldRoomPosition.DimensionY + oldRoomPosition.Height / 2,
                    Width = oldRoomPosition.Width,
                    Height = oldRoomPosition.Height / 2,
                    RoomId = newRoom.Id
                };
                oldRoomPosition.Height = oldRoomPosition.Height / 2;
            }
            else
            {
                newRoomPosition = new RoomPosition()
                {
                    DimensionX = oldRoomPosition.DimensionX + oldRoomPosition.Width / 2,
                    DimensionY = oldRoomPosition.DimensionY,
                    Width = oldRoomPosition.Width / 2,
                    Height = oldRoomPosition.Height,
                    RoomId = newRoom.Id
                };
                oldRoomPosition.Width = oldRoomPosition.Width / 2;
            }

            roomPositionsWriteRepo.Update(oldRoomPosition);
            roomPositionsWriteRepo.Add(newRoomPosition);
        }

        public void MergeRooms(RoomRenovationEvent roomRenovationEvent)
        {
            Room firstRoom = roomRenovationEvent.Room;
            Room secondRoom = roomRenovationEvent.MergeRoom;

            TransferInventory(firstRoom, secondRoom);

            GetNewPositionWhenMerging(firstRoom, secondRoom);

            //update nove sobe
            var roomWriteRepo = uow.GetRepository<IRoomWriteRepository>();
            if (firstRoom.BuildingName == "Building 1")
            {
                firstRoom.Height += secondRoom.Height;
            }
            else
            {
                firstRoom.Width += secondRoom.Width;
            }

            roomWriteRepo.Update(firstRoom);
            //brisanje stare
            roomWriteRepo.Delete(secondRoom);
        }

        private void GetNewPositionWhenMerging(Room firstRoom, Room secondRoom)
        {


            RoomPosition newPosition;

            var roomPositionsWriteRepo = uow.GetRepository<IRoomPositionWriteRepository>();
            var roomPositionReadRepo = uow.GetRepository<IRoomPositionReadRepository>();
            var firstRoomPosition = roomPositionReadRepo.GetByRoom(firstRoom.Id);
            var secondRoomPosition = roomPositionReadRepo.GetByRoom(secondRoom.Id);

            if (firstRoomPosition.Room.BuildingName == "Building 1")
            {
                if (firstRoomPosition.DimensionY < secondRoomPosition.DimensionY)
                    newPosition = new RoomPosition()
                    {
                        DimensionX = firstRoomPosition.DimensionX,
                        DimensionY = firstRoomPosition.DimensionY,
                        Width = firstRoomPosition.Width,
                        Height = firstRoomPosition.Height + secondRoomPosition.Height,
                        RoomId = firstRoomPosition.RoomId
                    };
                else
                    newPosition = new RoomPosition()
                    {
                        DimensionX = secondRoomPosition.DimensionX,
                        DimensionY = secondRoomPosition.DimensionY,
                        Width = secondRoomPosition.Width,
                        Height = firstRoomPosition.Height + secondRoomPosition.Height,
                        RoomId = firstRoomPosition.RoomId
                    };
            }
            else
            {
                if (firstRoomPosition.DimensionX < secondRoomPosition.DimensionX)
                    newPosition = new RoomPosition()
                    {
                        DimensionX = firstRoomPosition.DimensionX,
                        DimensionY = firstRoomPosition.DimensionY,
                        Width = firstRoomPosition.Width + secondRoomPosition.Width,
                        Height = firstRoomPosition.Height,
                        RoomId = firstRoomPosition.RoomId
                    };
                else
                    newPosition = new RoomPosition()
                    {
                        DimensionX = secondRoomPosition.DimensionX,
                        DimensionY = secondRoomPosition.DimensionY,
                        Width = firstRoomPosition.Width + secondRoomPosition.Width,
                        Height = secondRoomPosition.Height,
                        RoomId = firstRoomPosition.RoomId
                    };
            }
            // upis nove pozicije
            roomPositionsWriteRepo.Add(newPosition);
            //brisanje starih pozicija
            roomPositionsWriteRepo.Delete(firstRoomPosition);
            roomPositionsWriteRepo.Delete(secondRoomPosition);
        }

        private void TransferInventory(Room firstRoom, Room secondRoom)
        {

            var roomInventoryRepo = uow.GetRepository<IRoomInventoryWriteRepository>();

            foreach (RoomInventory item in secondRoom.RoomInventories)
            {
                bool found = false;
                foreach (RoomInventory ritem in firstRoom.RoomInventories)
                {
                    if (ritem.InventoryItemId == item.InventoryItemId)
                    {
                        ritem.Amount += item.Amount;
                        roomInventoryRepo.Update(ritem);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    item.RoomId = firstRoom.Id;
                    roomInventoryRepo.Update(item);
                }
            }
        }
    }
}
