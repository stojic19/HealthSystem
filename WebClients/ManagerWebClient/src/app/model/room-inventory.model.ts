import { InventoryItem } from "./inventory-item.model";

export class RoomInventory {
    id : number;
    roomId : number;
    inventoryItemId : number;
    amount : number;
    inventoryItem : InventoryItem;
}
