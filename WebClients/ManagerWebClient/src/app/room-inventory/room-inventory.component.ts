import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RoomInventory } from '../model/room-inventory.model';
import { RoomInventoriesService } from '../services/room-inventories.service';

@Component({
  selector: 'app-room-inventory',
  templateUrl: './room-inventory.component.html',
  styleUrls: [
    './room-inventory.component.css',
    '../hospital-equipment/hospital-equipment.component.css',
  ],
})
export class RoomInventoryComponent implements OnInit {
  public id: number;
  public roomInventories: RoomInventory[];
  isLoading = true;
  constructor(
    public roomInventoryService: RoomInventoriesService,
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe((params) => {
      this.id = +params['id'];
    });
  }

  ngOnInit(): void {
    this.roomInventoryService
      .getRoomInventory(this.id)
      .toPromise()
      .then((res) => {
        this.roomInventories = res as RoomInventory[];
        this.isLoading = false;
      });
  }
}
