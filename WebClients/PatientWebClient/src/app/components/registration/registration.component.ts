import { Component, Input, OnChanges, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { IChosenDoctor } from 'src/app/interfaces/chosen-doctor';
import {
  BloodType,
  IMedicalRecord,
  JobStatus,
} from 'src/app/interfaces/medical-record';
import { IMedicationIngredient } from 'src/app/interfaces/medication-ingredient';
import { INewAllergy } from 'src/app/interfaces/new-allergy';
import { INewPatient, Gender } from 'src/app/interfaces/new-patient';
import { AllergensService } from 'src/app/services/AllergenService/allergens.service';
import { ChosenDoctorService } from 'src/app/services/ChosenDoctorService/chosen-doctor.service';
import { RegistrationService } from 'src/app/services/PatientService/registration.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})
export class RegistrationComponent implements OnInit {
  newPatient!: INewPatient;
  doctors!: IChosenDoctor[];
  allergens!: IMedicationIngredient[];
  createForm!: FormGroup;
  passMatch: boolean = false;
  gender = Object.values(Gender).filter((value) => typeof value === 'string');
  bloodType = Object.values(BloodType).filter(
    (value) => typeof value === 'string'
  );
  constructor(
    private formBuilder: FormBuilder,
    private doctorService: ChosenDoctorService,
    private allergensService: AllergensService,
    private registrationService: RegistrationService
  ) {
    this.newPatient = {} as INewPatient;
    this.newPatient.medicalRecord = {} as IMedicalRecord;
  }

  ngOnInit(): void {
    this.createForm = this.formBuilder.group({
      firstName: new FormControl('', [
        Validators.required,
        Validators.pattern('^[A-ZŠĐŽČĆ][a-zšđćčžA-ZŠĐŽČĆ ]*$'),
      ]),
      lastName: new FormControl('', [
        Validators.required,
        Validators.pattern('^[A-ZŠĐŽČĆ][a-zšđćčžA-ZŠĐŽČĆ ]*$'),
      ]),
      middleName: new FormControl('', [
        Validators.required,
        Validators.pattern('^[A-ZŠĐŽČĆ][a-zšđćčžA-ZŠĐŽČĆ ]*$'),
      ]),
      gender: new FormControl(null, [Validators.required]),
      dateOfBirth: new FormControl(null, [Validators.required]),
      street: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-ZŠĐŽČĆ][a-zšđćčžA-ZŠĐŽČĆ ]*$'),
      ]),
      streetNumber: new FormControl(null, [
        Validators.required,
        Validators.pattern('^\\d{1,3}$'),
      ]),
      city: new FormControl(null, [
        Validators.required,
        Validators.pattern('^[A-ZŠĐŽČĆ][a-zšđćčžA-ZŠĐŽČĆ ]*$'),
      ]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      phone: new FormControl(null, [Validators.required]),
      height: new FormControl(null, [Validators.pattern('^\\d{1,3}$')]),
      weight: new FormControl(null, [Validators.pattern('^\\d{1,3}$')]),
      jobStatus: new FormControl(null, [Validators.required]),
      bloodType: new FormControl(null, [Validators.required]),
      drRight: new FormControl(null, [Validators.required]),
      allergens: new FormControl(null),
      username: new FormControl(null, [
        Validators.required,
        Validators.minLength(6),
      ]),
      password: new FormControl(null, [
        Validators.required,
        Validators.minLength(8),
      ]),
      passConfirmed: new FormControl(null, [
        Validators.required,
        Validators.minLength(8),
      ]),
    });

    this.doctorService.getAllNonLoaded().subscribe((res) => {
      this.doctors = res;
    });

    this.allergensService.getAll().subscribe((res) => {
      this.allergens = res;
    });
  }

  onSubmit(): void {
    this.createPatient();
    this.registrationService
      .registerPatient(this.newPatient)
      .subscribe((res) => console.log(res));
  }

  getDr(event: any) {
    this.newPatient.medicalRecord.doctorId = event.value;
  }

  getAllergens(event: any) {
    this.newPatient.medicalRecord.allergies = [] as INewAllergy[];
    let ids = [];
    ids = event.value.toString().split(',');
    ids.forEach((id: number) => {
      let allergy = {} as INewAllergy;
      allergy.medicalIngredientId = id;
      this.newPatient.medicalRecord.allergies.push(allergy);
    });
  }

  getBloodType(event: any) {
    this.newPatient.medicalRecord.bloodType = event.value as BloodType;
  }

  getGender(event: any) {
    this.newPatient.gender = event.value as Gender;
  }

  getjobStatus(event: any) {
    this.newPatient.medicalRecord.jobStatus = event.value as JobStatus;
  }

  onPasswordInput(): void {
    this.passMatch =
      this.createForm.value.password === this.createForm.value.passConfirmed;
  }

  createPatient(): void {
    this.newPatient.firstName = this.createForm.value.firstName;
    this.newPatient.lastName = this.createForm.value.lastName;
    this.newPatient.middleName = this.createForm.value.middleName;
    this.newPatient.street = this.createForm.value.street;
    this.newPatient.streetNumber = this.createForm.value.streetNumber;
    this.newPatient.cityId = 1;
    this.newPatient.dateOfBirth = new Date(
      this.createForm.value.dateOfBirth.getTime() -
        this.createForm.value.dateOfBirth.getTimezoneOffset() * 60000
    );
    //this.newPatient.medicalRecordId = 9;
    this.newPatient.medicalRecord.weight = this.createForm.value.weight;
    this.newPatient.medicalRecord.height = this.createForm.value.height;
    this.newPatient.phoneNumber = this.createForm.value.phoneNumber;
    this.newPatient.email = this.createForm.value.email;
    this.newPatient.userName = this.createForm.value.username;
    this.newPatient.password = this.createForm.value.password;
  }
}
