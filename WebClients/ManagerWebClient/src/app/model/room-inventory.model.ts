import { Room } from "../interfaces/room";
import { InventoryItem } from "./inventory-item.model";

export class RoomInventory {
    id : number;
    roomId : number;
    room: Room;
    inventoryItemId : number;
    amount : number;
    inventoryItem : InventoryItem;
}
