import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { EquipmentTransferEvent } from '../model/equipment-transfer-event';
import { RoomRenovationEvent } from '../model/room-renovation-event';
import { RoomScheduleService } from '../services/room-schedule.service';
import {
  ConfirmDialogComponent,
  ConfirmDialogModel,
} from './confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-room-schedule',
  templateUrl: './room-schedule.component.html',
  styleUrls: ['./room-schedule.component.css'],
})
export class RoomScheduleComponent implements OnInit {
  public roomId: number;

  constructor(
    public roomScheduleService: RoomScheduleService,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) {
    this.route.params.subscribe((params) => {
      this.roomId = +params['id'];
    });
  }

  equipmentTransferEvents: EquipmentTransferEvent[];
  renovationEvents: RoomRenovationEvent[];
  isLoading = true;
  selectedEvent: EquipmentTransferEvent;
  result: boolean;

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

  confirmTransferCancelDialog(): void {
    const message = 'Are you sure you want to cancel selected transfer?';

    const dialogData = new ConfirmDialogModel(
      'Confirm Cancelling Transfer Event',
      message
    );

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: '600px',
      data: dialogData,
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      this.result = dialogResult;
      console.log(this.result);
    });
  }

  confirmRenovationCancelDialog(): void {
    const message = 'Are you sure you want to cancel selected renovation?';

    const dialogData = new ConfirmDialogModel(
      'Confirm Cancelling room renovation',
      message
    );

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: '600px',
      data: dialogData,
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      this.result = dialogResult;
    });
  }
}
