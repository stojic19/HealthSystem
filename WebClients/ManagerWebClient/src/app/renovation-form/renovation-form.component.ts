import { Component, OnInit } from '@angular/core';
import { Room } from '../interfaces/room';

@Component({
  selector: 'app-renovation-form',
  templateUrl: './renovation-form.component.html',
  styleUrls: ['./renovation-form.component.css']
})
export class RenovationFormComponent implements OnInit {

  public step = 1;
  public renovationType="merge"
  public typeR = ""
  chosenRoom: Room;

  constructor() { }

  ngOnInit(): void {
  }

  nextStep(){
    this.step++;
  }

  prevStep(){
    this.step--;
    this.typeR = this.renovationType; 
  }

}
