using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.GraphicalEditor.Service
{
    public class FindingSurroundingRoomsService
    {
        private readonly IUnitOfWork uow;
        public FindingSurroundingRoomsService(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }
        
        public IEnumerable<Room> GetSurroundingRooms(Room room)
        {
            var roomRepo = uow.GetRepository<IRoomReadRepository>();
            List<Room> foundRooms = new();
            foreach (Room r in roomRepo.GetAll().ToList())
            {
                if (r.AreNeighbors(room))
                {
                    foundRooms.Add(room);
                }
            }
            return foundRooms;
        }
    }
 }
