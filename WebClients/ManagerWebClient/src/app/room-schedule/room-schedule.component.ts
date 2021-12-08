import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EquipmentTransferEvent } from '../model/equipment-transfer-event';
import { RoomRenovationEvent } from '../model/room-renovation-event';
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
  renovationEvents: RoomRenovationEvent[];
  isLoading = true;
  selectedEvent: EquipmentTransferEvent;

  ngOnInit(): void {
    this.getAllEquipmentTransfers();
    this.getAllRenovations();
  }

  getAllEquipmentTransfers() {
    this.roomScheduleService
      .getTransferEventsByRoom(this.roomId)
      .toPromise()
      .then((res) => {
        this.equipmentTransferEvents = res as EquipmentTransferEvent[];
        this.isLoading = false;
      });
  }

  getAllRenovations() {
    this.isLoading = false;
    this.roomScheduleService
      .getRenovationsByRoom(this.roomId)
      .toPromise()
      .then((res) => {
        this.renovationEvents = res as RoomRenovationEvent[];
        this.isLoading = false;
      });
  }

  getAllAppointments() {
    this.isLoading = false;
    //dodati ucitavanje termina
  }
}
