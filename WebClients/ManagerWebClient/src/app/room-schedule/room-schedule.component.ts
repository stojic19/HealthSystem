import { Component, OnInit } from '@angular/core';
import { throwMatDuplicatedDrawerError } from '@angular/material/sidenav';
import { ActivatedRoute } from '@angular/router';
import { EquipmentTransferEvent } from '../model/equipment-transfer-event';
import { RoomInventoriesService } from '../services/room-inventories.service';
import { RoomScheduleService } from '../services/room-schedule.service';

@Component({
  selector: 'app-room-schedule',
  templateUrl: './room-schedule.component.html',
  styleUrls: ['./room-schedule.component.css'],
})
export class RoomScheduleComponent implements OnInit {
  public roomId: number;

  constructor(
    public roomScheduleService: RoomScheduleService,
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe((params) => {
      this.roomId = +params['id'];
    });
  }

  equipmentTransferEvents: EquipmentTransferEvent[];
  isLoading = true;
  selectedEvent: EquipmentTransferEvent;

  ngOnInit(): void {
    this.getAllScheduledEvents();
  }

  getAllScheduledEvents() {
    this.isLoading = false;
    this.roomScheduleService
      .getTransferEventsByRoom(this.roomId)
      .toPromise()
      .then((res) => {
        this.equipmentTransferEvents = res as EquipmentTransferEvent[];
        this.isLoading = false;
      });
  }
}
