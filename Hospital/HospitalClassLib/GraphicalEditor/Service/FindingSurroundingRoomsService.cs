using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.GraphicalEditor.Service
{
    public class FindingSurroundingRoomsService
    {
        private readonly IUnitOfWork uow;
        public FindingSurroundingRoomsService(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        public IEnumerable<Room> GetSurroundingRooms(RoomPosition roomPosition)
        {
            var roomRepo = uow.GetRepository<IRoomReadRepository>();
            var repo = uow.GetRepository<IRoomPositionReadRepository>();
            List<Room> foundRooms = new List<Room>();
            foreach (RoomPosition position in repo.GetAll().ToList())
            {
                if (AreNeighbors(roomPosition, position))
                {
                    var room = roomRepo.GetById(position.RoomId);
                    foundRooms.Add(room);
                }
            }
            return foundRooms;
        }

        public bool AreNeighbors(RoomPosition firstPosition, RoomPosition secondPosition)
        {
            var roomRepo = uow.GetRepository<IRoomReadRepository>();
            var firstRoom = roomRepo.GetById(firstPosition.RoomId);
            var secondRoom = roomRepo.GetById(secondPosition.RoomId);

            if (!firstRoom.BuildingName.Equals(secondRoom.BuildingName))
                return false;
            else if (firstRoom.FloorNumber != secondRoom.FloorNumber)
                return false;
            else if (firstRoom.BuildingName.Equals("Building 1"))
            {
                if (firstPosition.DimensionY + firstPosition.Height == secondPosition.DimensionY)
                {
                    return true;
                }
                else if (firstPosition.DimensionY - firstPosition.Height == secondPosition.DimensionY)
                {
                    return true;
                }
            }
            else if (firstRoom.BuildingName.Equals("Building 2"))
            {
                if (firstPosition.DimensionX + firstPosition.Width == secondPosition.DimensionX)
                {
                    return true;
                }
                else if (firstPosition.DimensionX - firstPosition.Width == secondPosition.DimensionX)
                {
                    return true;
                }
            }
            return false;
        }
    }
 }
