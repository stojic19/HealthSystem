import { RoomPosition } from "../model/room-position.model";

export enum RoomType {
  AppointmentRoom,
  OperationRoom,
  Storage,
  Bedroom,
  OfficeRoom,
}

export class Room {
    id! : number;
    name! : string;
    description! : string;
    width! : number;
    height! : number;
    floorNumber! : number;
    buildingName! : string;
    doctors! : [];
    roomInventories! : [];
    scheduledEvents! : [];
    roomType: number;
    roomPosition: RoomPosition;
}
