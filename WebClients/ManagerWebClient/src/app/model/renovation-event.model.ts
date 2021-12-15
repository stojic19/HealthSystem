import { Room, RoomType } from "../interfaces/room";

export class RenovationEvent {
    startDate: Date;
    endDate: Date;
    roomId: number;
    isMerge: boolean;
    mergeRoomId: number;
    firstRoomName: string;
    firstRoomDescription: string;
    firstRoomType: RoomType;
    secondRoomName: string; 
    secondRoomDescription: string;
    secondRoomType: RoomType;
}
