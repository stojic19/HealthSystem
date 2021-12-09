using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using Hospital.RoomsAndEquipment.Model;
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
            var repo = uow.GetRepository<IRoomPositionReadRepository>();
            List<Room> foundRooms = new List<Room>();
            foreach (RoomPosition position in repo.GetAll().ToList())
            {
                if (AreNeighbors(roomPosition, position))
                {
                    foundRooms.Add(position.Room);
                }
            }
            return foundRooms;
        }

        public bool AreNeighbors(RoomPosition firstPosition, RoomPosition secondPosition)
        {
            if (!firstPosition.Room.BuildingName.Equals(secondPosition.Room.BuildingName))
                return false;
            else if (firstPosition.Room.FloorNumber != secondPosition.Room.FloorNumber)
                return false;
            else if (firstPosition.Room.BuildingName.Equals("Building 1"))
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
            else if (firstPosition.Room.BuildingName.Equals("Building 2"))
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
