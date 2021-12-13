import { Component, OnInit } from '@angular/core';
import { throwMatDialogContentAlreadyAttachedError } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Room, RoomType } from '../interfaces/room';
import { RenovationEvent } from '../model/renovation-event.model';
import { RenovationTermsRequest } from '../model/renovation-terms-request.model';
import { TimePeriod } from '../model/time-period';
import { TimePeriodView } from '../model/time-period-view';
import { RoomRenovationService } from '../services/room-renovation.service';
import { RoomsService } from '../services/rooms.service';

@Component({
  selector: 'app-renovation-form',
  templateUrl: './renovation-form.component.html',
  styleUrls: ['./renovation-form.component.css']
})
export class RenovationFormComponent implements OnInit {

  public step = 1;
  public renovationType = "merge"
  public typeR = ""
  chosenRoom: Room;
  surroundingRoom: Room;

  duration: number;
  startDate: Date;
  endDate: Date;

  firstRoomName: string;
  firstRoomDescription: string;
  firstRoomType: string;

  secondRoomName: string;
  secondRoomDescription: string;
  secondRoomType: string;

  availableTerms: TimePeriod[];
  availableTermsView: TimePeriodView[];
  terms: TimePeriod[];
  selectedTerm: TimePeriod;

  constructor(
    public service: RoomRenovationService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  nextStep() {
    if (this.step == 5) {
      this.getAvailableTerms();
    }

    this.step++;
  }

  prevStep() {

    this.step--;
    this.typeR = this.renovationType;
    if (this.step == 1 || this.step == 2) {
      this.chosenRoom = new Room();
    }
    if (this.step == 3 && this.renovationType == 'merge') {
      this.surroundingRoom = new Room();
    }
    if ((this.step == 3 && this.renovationType == 'split') || (this.step == 4 && this.renovationType == 'merge')) {
      this.duration = 0;
      this.startDate = new Date();
      this.endDate = new Date();
    }
    if (this.step == 4 && this.renovationType == 'split') {
      this.firstRoomName = '';
      this.firstRoomDescription = '';
      this.firstRoomType = '';
    }
    if (this.step == 5 && this.renovationType == 'split') {
      this.secondRoomName = '';
      this.secondRoomDescription = '';
      this.secondRoomType = '';
      this.selectedTerm = new TimePeriod();
    }
    if (this.step == 5 && this.renovationType == 'merge') {
      this.firstRoomName = '';
      this.firstRoomDescription = '';
      this.firstRoomType = '';
      this.selectedTerm = new TimePeriod();
    }

  }

  isButtonDisabled() {
    if (
      (JSON.stringify(this.chosenRoom) === '{}' ||
        this.chosenRoom == null ||
        this.chosenRoom == undefined) &&
      this.step == 2
    )
      return true;

    if (
      (JSON.stringify(this.selectedTerm) === '{}' ||
        this.selectedTerm == null ||
        this.selectedTerm == undefined) &&
      this.step == 6
    )
      return true;

    if (
      (JSON.stringify(this.surroundingRoom) === '{}' ||
        this.surroundingRoom == null ||
        this.surroundingRoom == undefined) &&
      (this.step == 3 && this.renovationType == 'merge')
    )
      return true;

    if (
      (JSON.stringify(this.surroundingRoom) === '{}' ||
        this.surroundingRoom == null ||
        this.surroundingRoom == undefined) &&
      (this.step == 4 && this.renovationType == 'merge')
    )
      return true;

    if (
      (this.step == 3 && this.renovationType == 'split') &&
      (this.startDate === undefined ||
        this.endDate === undefined ||
        this.duration === undefined ||
        this.duration <= 0 ||
        this.endDate <= this.startDate)
    )
      return true;

    if (
      (this.step == 4 && this.renovationType == 'merge') &&
      (this.startDate === undefined ||
        this.endDate === undefined ||
        this.duration === undefined ||
        this.duration <= 0 ||
        this.endDate <= this.startDate)
    )
      return true;

    if ((this.step == 4 && this.renovationType == 'split') &&
      (this.firstRoomName === undefined ||
        this.firstRoomDescription === undefined ||
        this.firstRoomType === undefined ||
        this.firstRoomName === '' ||
        this.firstRoomDescription === '' ||
        this.firstRoomType === '')
    ) return true;

    if ((this.step == 5 && this.renovationType == 'merge') &&
      (this.firstRoomName === undefined ||
        this.firstRoomDescription === undefined ||
        this.firstRoomType === undefined ||
        this.firstRoomName === '' ||
        this.firstRoomDescription === '' ||
        this.firstRoomType === '')
    ) return true;

    if ((this.step == 5 && this.renovationType == 'split') &&
      (this.secondRoomName === undefined ||
        this.secondRoomDescription === undefined ||
        this.secondRoomType === undefined ||
        this.secondRoomName === '' ||
        this.secondRoomDescription === '' ||
        this.secondRoomType === '')
    ) return true;

    if (this.step == 6 && this.selectedTerm == undefined)
      return true;

    return false;
  }

  getAvailableTerms() {

    let request = new RenovationTermsRequest();

    if (this.renovationType == 'split') {
      request = {
        startDate: this.startDate,
        endDate: this.endDate,
        duration: this.duration,
        chosenRoomId: this.chosenRoom.id,
        surroundingRoomId: this.chosenRoom.id
      };
    }

    if (this.renovationType == 'merge') {
      request = {
        startDate: this.startDate,
        endDate: this.endDate,
        duration: this.duration,
        chosenRoomId: this.chosenRoom.id,
        surroundingRoomId: this.surroundingRoom.id
      };
    }


    this.service
      .getAvailableTerms(request)
      .toPromise()
      .then((result) => {
        this.availableTermsView = result as TimePeriodView[];
        this.availableTerms = [];
        for (let term of this.availableTermsView) {
          var newTerm: TimePeriod = {
            startDate: new Date(term.startDate),
            endDate: new Date(term.endDate),
          };
          this.availableTerms.push(newTerm);
        }
      });
  }

  createRenovationRequest() {
    let request = new RenovationEvent();

    var firstType: RoomType;

    switch (this.firstRoomType) {
      case 'AppointmentRoom': {
        firstType = RoomType.AppointmentRoom;
        break;
      }
      case 'OperationRoom': {
        firstType = RoomType.OperationRoom;
        break;
      }
      case 'Storage': {
        firstType = RoomType.Storage;
        break;
      }
      case 'Bedroom': {
        firstType = RoomType.Bedroom;
        break;
      }
      case 'Office': {
        firstType = RoomType.OfficeRoom;
        break;
      }
      default:
        firstType = RoomType.AppointmentRoom;

    }
    if (this.renovationType == 'split') {
      var secondType: RoomType;

      switch (this.secondRoomType) {
        case 'AppointmentRoom': {
          secondType = RoomType.AppointmentRoom;
          break;
        }
        case 'OperationRoom': {
          secondType = RoomType.OperationRoom;
          break;
        }
        case 'Storage': {
          secondType = RoomType.Storage;
          break;
        }
        case 'Bedroom': {
          secondType = RoomType.Bedroom;
          break;
        }
        case 'Office': {
          secondType = RoomType.OfficeRoom;
          break;
        }
        default:
          secondType = RoomType.AppointmentRoom;

      }

      request = {
        startDate: this.selectedTerm.startDate,
        endDate: this.selectedTerm.endDate,
        roomId: this.chosenRoom.id,
        isMerge: false,
        mergeRoomId: this.chosenRoom.id,
        firstRoomName: this.firstRoomName,
        firstRoomDescription: this.firstRoomDescription,
        firstRoomType: firstType,
        secondRoomName: this.secondRoomName,
        secondRoomDescription: this.secondRoomDescription,
        secondRoomType: secondType
      };
    } else {
      request = {
        startDate: this.selectedTerm.startDate,
        endDate: this.selectedTerm.endDate,
        roomId: this.chosenRoom.id,
        isMerge: true,
        mergeRoomId: this.surroundingRoom.id,
        firstRoomName: this.firstRoomName,
        firstRoomDescription: this.firstRoomDescription,
        firstRoomType: firstType,
        secondRoomName: this.firstRoomName,
        secondRoomDescription: this.firstRoomDescription,
        secondRoomType: firstType
      };
    }

    this.service.addRenovationEvent(request);
  }

  goBack() {
    this.router.navigate(['/home']);
  }

}
