import { Component, OnInit } from '@angular/core';
import { Room } from '../interfaces/room';

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

  constructor() { }

  ngOnInit(): void {
  }

  nextStep() {
    this.step++;
  }

  prevStep() {

    this.step--;
    this.typeR = this.renovationType;
    if (this.step == 1 || this.step == 2) {
      this.chosenRoom = new Room();
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
      (JSON.stringify(this.surroundingRoom) === '{}' ||
        this.surroundingRoom == null ||
        this.surroundingRoom == undefined) &&
      this.step == 3
    )
      return true;

    return false;
  }

}
