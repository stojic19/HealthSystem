export class EquipmentTransferEvent {
  startDate: Date;
  endDate: Date;
  initialRoomId: number;
  destinationRoomId: number;
  inventoryItemId: number;
  quantity: number;
}

export class EquipmentTransferEventDTO {
  id: number;
  startDate: Date;
  endDate: Date;
  initialRoomId: number;
  destinationRoomId: number;
  inventoryItemId: number;
  quantity: number;
}
