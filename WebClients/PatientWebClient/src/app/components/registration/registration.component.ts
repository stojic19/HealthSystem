import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { INewPatient } from 'src/app/interfaces/new-patient';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})
export class RegistrationComponent implements OnInit {
  newPatient!: INewPatient;
  dateOfBirth = new FormControl(new Date()); // dateOfBirth.value

  constructor() {
    this.newPatient = {} as INewPatient;
  }

  ngOnInit(): void { }

  register(): void {
    console.log();
  }
}
