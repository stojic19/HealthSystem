using Hospital.SharedModel.Model.Wrappers;
using System;

namespace Hospital.RoomsAndEquipment.Model
{
    public class EquipmentTransferEvent
    {
        public int Id { get; private set; }
        public TimePeriod TimePeriod { get; private set; }
        public int? InitialRoomInventoryId { get; private set; }
        public RoomInventory InitialRoomInventory { get; private set; }
        public int? DestinationRoomInventoryId { get; private set; }
        public RoomInventory DestinationRoomInventory { get; private set; }
        public int Quantity { get; private set; }

        public EquipmentTransferEvent() { }

        public EquipmentTransferEvent(int id, TimePeriod timePeriod, int initialRoomInventoryId, RoomInventory initialRoomInventory,
                                        int destinationRoomInvenotoryId, RoomInventory destinationRoomInventory, int quantity) {
            Id = id;
            TimePeriod = timePeriod;
            InitialRoomInventoryId = initialRoomInventoryId;
            InitialRoomInventory = initialRoomInventory;
            DestinationRoomInventoryId = destinationRoomInvenotoryId;
            DestinationRoomInventory = destinationRoomInventory;
            Quantity = quantity;
            Validate();
        }

        public EquipmentTransferEvent(TimePeriod timePeriod, int initialRoomInventoryId, RoomInventory initialRoomInventory,
                                        int destinationRoomInvenotoryId, RoomInventory destinationRoomInventory, int quantity)
        {
            TimePeriod = timePeriod;
            InitialRoomInventoryId = initialRoomInventoryId;
            InitialRoomInventory = initialRoomInventory;
            DestinationRoomInventoryId = destinationRoomInvenotoryId;
            DestinationRoomInventory = destinationRoomInventory;
            Quantity = quantity;
            Validate();
        }

        public EquipmentTransferEvent(int id, TimePeriod timePeriod, RoomInventory initialRoomInventory,
                                        RoomInventory destinationRoomInventory, int quantity)
        {
            Id = id;
            TimePeriod = timePeriod;
            InitialRoomInventory = initialRoomInventory;
            DestinationRoomInventory = destinationRoomInventory;
            Quantity = quantity;
            Validate();
        }

        public EquipmentTransferEvent(TimePeriod timePeriod, RoomInventory initialRoomInventory,
                                        RoomInventory destinationRoomInventory, int quantity)
        {
            TimePeriod = timePeriod;
            InitialRoomInventory = initialRoomInventory;
            DestinationRoomInventory = destinationRoomInventory;
            Quantity = quantity;
            Validate();
        }

        private void Validate()
        {
            if (double.IsNegative(Quantity))
                throw new ArgumentException("Quantity of an item must be larger than 0!");
        }

        public RoomInventory TransferToDestinationRoom() {
            DestinationRoomInventory.Add(Quantity);
            return DestinationRoomInventory;
        }

        public RoomInventory TransferFromInitialRoom() {
            InitialRoomInventory.Substract(Quantity);
            return InitialRoomInventory;
        }
    }
}
