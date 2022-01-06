using Hospital.GraphicalEditor.Model;
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
            var writeRepo = uow.GetRepository<IRoomRenovationEventWriteRepository>();
            foreach (RoomRenovationEvent roomRenovationEvent in repo.GetAll().ToList())
            {
                if (DateTime.Compare(roomRenovationEvent.EndDate, DateTime.Now) <= 0)
                {
                    if (roomRenovationEvent.IsMerge)
                        MergeRooms(roomRenovationEvent);
                    else
                        SplitRoom(roomRenovationEvent);
                    writeRepo.Delete(roomRenovationEvent);
                }
            }
        }

        public void SplitRoom(RoomRenovationEvent roomRenovationEvent)
        {
            var roomRepo = uow.GetRepository<IRoomReadRepository>();
            Room selectedRoom = roomRepo.GetById((int)roomRenovationEvent.RoomId);
            double width;
            double height;
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
            RoomPosition newRoomPosition = GetNewPositionsWhenSplitting(selectedRoom);

            Room newRoom = new Room()
            {
                Name = roomRenovationEvent.FirstRoomName,
                Description = roomRenovationEvent.FirstRoomDescription,
                BuildingName = selectedRoom.BuildingName,
                FloorNumber = selectedRoom.FloorNumber,
                RoomType = roomRenovationEvent.FirstRoomType,
                Width = width,
                Height = height,
                RoomPosition = newRoomPosition
            };

            var roomWriteRepo = uow.GetRepository<IRoomWriteRepository>();
            roomWriteRepo.Add(newRoom);
            selectedRoom.Name = roomRenovationEvent.SecondRoomName;
            selectedRoom.Description = roomRenovationEvent.SecondRoomDescription;
            selectedRoom.RoomType = roomRenovationEvent.SecondRoomType;
            roomWriteRepo.Update(selectedRoom);
            
        }

        private RoomPosition GetNewPositionsWhenSplitting(Room selectedRoom)
        {
            RoomPosition newRoomPosition;

            if (selectedRoom.BuildingName == "Building 1")
            {
                double x = selectedRoom.RoomPosition.DimensionX;
                double y = selectedRoom.RoomPosition.DimensionY + selectedRoom.RoomPosition.Height / 2;
                double width = selectedRoom.RoomPosition.Width;
                double height = selectedRoom.RoomPosition.Height / 2;

                newRoomPosition = new RoomPosition(x, y, width, height);
                selectedRoom.RoomPosition.AddHeight(-selectedRoom.RoomPosition.Height / 2); 
            }
            else
            {
                double x = selectedRoom.RoomPosition.DimensionX + selectedRoom.RoomPosition.Width / 2;
                double y = selectedRoom.RoomPosition.DimensionY;
                double width = selectedRoom.RoomPosition.Width / 2;
                double height = selectedRoom.RoomPosition.Height;
                newRoomPosition = new RoomPosition(x, y, width, height);
                selectedRoom.RoomPosition.AddWidth(-selectedRoom.RoomPosition.Width / 2);
            }

            var roomWriteRepo = uow.GetRepository<IRoomWriteRepository>();
            roomWriteRepo.Update(selectedRoom);

            return newRoomPosition;
        }

        public void MergeRooms(RoomRenovationEvent roomRenovationEvent)
        {
            var roomRepo = uow.GetRepository<IRoomReadRepository>();
            Room firstRoom = roomRepo.GetById((int)roomRenovationEvent.RoomId);
            Room secondRoom = roomRepo.GetById((int)roomRenovationEvent.MergeRoomId);

            TransferInventory(firstRoom, secondRoom);

            RoomPosition newRoomPosition = GetNewPositionWhenMerging(firstRoom, secondRoom);

            var roomWriteRepo = uow.GetRepository<IRoomWriteRepository>();
            if (firstRoom.BuildingName == "Building 1")
            {
                firstRoom.Height += secondRoom.Height;
            }
            else
            {
                firstRoom.Width += secondRoom.Width;
            }

            firstRoom.Name = roomRenovationEvent.FirstRoomName;
            firstRoom.Description = roomRenovationEvent.FirstRoomDescription;
            firstRoom.RoomType = roomRenovationEvent.FirstRoomType;
            firstRoom.RoomPosition = newRoomPosition;
            roomWriteRepo.Update(firstRoom);
            roomWriteRepo.Delete(secondRoom);
        }

        private RoomPosition GetNewPositionWhenMerging(Room firstRoom, Room secondRoom)
        {


            RoomPosition newPosition;

            if (firstRoom.BuildingName == "Building 1")
            {
                if (firstRoom.RoomPosition.DimensionY < secondRoom.RoomPosition.DimensionY)
                    newPosition = firstRoom.RoomPosition.AddHeight(secondRoom.RoomPosition.Height);
                else
                    newPosition = secondRoom.RoomPosition.AddHeight(firstRoom.RoomPosition.Height);
            }
            else
            {
                if (firstRoom.RoomPosition.DimensionX < secondRoom.RoomPosition.DimensionX)
                    newPosition = firstRoom.RoomPosition.AddWidth(secondRoom.RoomPosition.Width);
                else
                    newPosition = secondRoom.RoomPosition.AddWidth(firstRoom.RoomPosition.Width);
              
            }
            return newPosition;
        }

        private void TransferInventory(Room firstRoom, Room secondRoom)
        {

            var roomInventoryRepo = uow.GetRepository<IRoomInventoryWriteRepository>();
            var inventoryRepo = uow.GetRepository<IRoomInventoryReadRepository>();
            var firstRoomInventory = inventoryRepo.GetByRoom(firstRoom.Id);
            var secondRoomInventory = inventoryRepo.GetByRoom(secondRoom.Id);

            foreach (RoomInventory item in secondRoomInventory.ToList())
            {
                bool found = false;
                foreach (RoomInventory ritem in firstRoomInventory.ToList())
                {
                    if (ritem.InventoryItemId == item.InventoryItemId)
                    {
                        ritem.Add(item.Amount);
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
