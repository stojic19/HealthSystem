using Model;
using System;
using System.Collections.Generic;

namespace Repository.RoomInventoryPersistance
{
    public class RoomInventoryRepository : IRoomInventoryRepository
    {
        public void Create(RoomInventory newValue)
        {
            throw new NotImplementedException();
        }

        public void DeleteByEquality(RoomInventory roomInventory)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteByInventoryId(string inventoryId)
        {
            throw new NotImplementedException();
        }

        public void DeleteByRoomId(int id)
        {
            throw new NotImplementedException();
        }

        public List<RoomInventory> FindAllInventoryInRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public RoomInventory FindByBothIds(int roomId, string inventoryId)
        {
            throw new NotImplementedException();
        }

        public RoomInventory GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<RoomInventory> GetValues()
        {
            throw new NotImplementedException();
        }

        public void Save(List<RoomInventory> values)
        {
            throw new NotImplementedException();
        }

        public void SetNewQuantity(RoomInventory roomInventory, int newQuantity)
        {
            throw new NotImplementedException();
        }

        public void Update(RoomInventory newValue)
        {
            throw new NotImplementedException();
        }
    }
}