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
  timePeriod: TimePeriod;
  initialRoomInventoryId: number;
  destinationRoomInventoryId: number;
  quantity: number;
}

export class TimePeriod{
  startTime : Date;
  endTime : Date;
}
