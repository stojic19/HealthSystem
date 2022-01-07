using Hospital.GraphicalEditor.Model;
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
        
        public IEnumerable<Room> GetSurroundingRooms(Room room)
        {
            var roomRepo = uow.GetRepository<IRoomReadRepository>();
            List<Room> foundRooms = new List<Room>();
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
