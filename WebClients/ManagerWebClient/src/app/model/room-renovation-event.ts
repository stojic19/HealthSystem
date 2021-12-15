import { Room } from '../interfaces/room';

export interface RoomRenovationEvent {
  id: number;
  startDate: Date;
  endDate: Date;
  roomId: number;
  room: Room;
  isMerge: boolean;
  mergeRoomId: number;
  mergeRoom: Room;
}
