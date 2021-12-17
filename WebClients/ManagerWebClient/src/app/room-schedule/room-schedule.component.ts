import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import {
  EquipmentTransferEvent,
  EquipmentTransferEventDTO,
} from '../model/equipment-transfer-event';
import { RoomRenovationEvent } from '../model/room-renovation-event';
import { Schedule } from '../model/schedule';
import { RoomScheduleService } from '../services/room-schedule.service';
import {
  ConfirmDialogComponent,
  ConfirmDialogModel,
} from './confirm-dialog/confirm-dialog.component';
import {
  DetailsDialogComponent,
  DetailsDialogModel,
} from './details-dialog/details-dialog.component';

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

  equipmentTransferEvents: EquipmentTransferEventDTO[];
  renovationEvents: RoomRenovationEvent[];
  scheduledEvents: Schedule[];
  isLoading = true;
  transferCancelResult: boolean;
  renovationCancelResult: boolean;

  ngOnInit(): void {
    this.getAllEquipmentTransfers();
    this.getAllRenovations();
    this.getAllAppointments();
  }

  getAllEquipmentTransfers() {
    this.roomScheduleService
      .getTransferEventsByRoom(this.roomId)
      .toPromise()
      .then((res) => {
        this.equipmentTransferEvents = res as EquipmentTransferEventDTO[];
        this.isLoading = false;
      });
  }

  getAllRenovations() {
    this.roomScheduleService
      .getRenovationsByRoom(this.roomId)
      .toPromise()
      .then((res) => {
        this.renovationEvents = res as RoomRenovationEvent[];
        this.isLoading = false;
      });
  }

  getAllAppointments() {
    this.roomScheduleService
      .getAppointmentsByRoom(this.roomId)
      .toPromise()
      .then((res) => {
        this.scheduledEvents = res as Schedule[];
        this.isLoading = false;
      });
  }

  confirmTransferCancelDialog(transfer: EquipmentTransferEventDTO): void {
    const message = 'Are you sure you want to cancel selected transfer?';

    const dialogData = new ConfirmDialogModel(
      'Confirm cancelling Transfer Event',
      message
    );

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: '600px',
      data: dialogData,
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      this.transferCancelResult = dialogResult;

      if (this.transferCancelResult) {
        this.roomScheduleService.cancelEquipmentTransferEvent(transfer);
      }

      setTimeout(() => {
        window.location.reload();
      }, 500);
    });
  }

  confirmRenovationCancelDialog(renovation: RoomRenovationEvent): void {
    const message = 'Are you sure you want to cancel selected renovation?';

    const dialogData = new ConfirmDialogModel(
      'Confirm cancelling room renovation',
      message
    );

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: '600px',
      data: dialogData,
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      this.renovationCancelResult = dialogResult;

      if (this.renovationCancelResult) {
        this.roomScheduleService.cancelRenovationEvent(renovation);
      }

      setTimeout(() => {
        window.location.reload();
      }, 500);
    });
  }

  openDialog(schedule: Schedule): void {
    const eventType = schedule.scheduledEventType;
    const startDate = schedule.startDate;
    const endDate = schedule.endDate;
    const roomName = schedule.room.name;
    const buildingName = schedule.room.buildingName;
    const patientFirstName = schedule.patient.firstName;
    const patientLastName = schedule.patient.lastName;
    const doctorFirstName = schedule.doctor.firstName;
    const doctorLastName = schedule.doctor.lastName;

    const dialogData = new DetailsDialogModel(
      eventType,
      startDate,
      endDate,
      roomName,
      buildingName,
      patientFirstName,
      patientLastName,
      doctorFirstName,
      doctorLastName
    );

    const dialogRef = this.dialog.open(DetailsDialogComponent, {
      maxWidth: '500px',
      height: '300px',
      data: dialogData,
    });
  }

  checkIfEventIsTomorrow(event: any) {
    const today = new Date();
    const tomorrow = new Date(today);
    tomorrow.setDate(tomorrow.getDate() + 1);
    const start = new Date(event.startDate);

    if (start.getTime() <= tomorrow.getTime()) {
      return false;
    }

    return true;
  }
}
