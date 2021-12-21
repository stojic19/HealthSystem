import { Component, Input, OnChanges, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IChosenDoctor } from 'src/app/interfaces/chosen-doctor';
import { ICity } from 'src/app/interfaces/city';
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
import { CityService } from 'src/app/services/CityService/city.service';
import { RegistrationService } from 'src/app/services/PatientService/registration.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})
export class RegistrationComponent implements OnInit {
  errorMessage!: string;
  newPatient!: INewPatient;
  doctors!: IChosenDoctor[];
  allergens!: IMedicationIngredient[];
  cities!: ICity[];
  createForm!: FormGroup;
  formData!: FormData;
  uploaded: boolean = false;
  fileToUpload!: File;
  passMatch: boolean = false;
  gender = Object.values(Gender).filter((value) => typeof value === 'string');
  bloodType = Object.values(BloodType).filter(
    (value) => typeof value === 'string'
  );
  constructor(
    private formBuilder: FormBuilder,
    private doctorService: ChosenDoctorService,
    private allergensService: AllergensService,
    private cityService: CityService,
    private registrationService: RegistrationService,
    private _snackBar: MatSnackBar,
    private router: Router
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
      city: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      phone: new FormControl(null, [Validators.required]),
      height: new FormControl(null, [Validators.pattern('^\\d{1,3}$')]),
      weight: new FormControl(null, [Validators.pattern('^\\d{1,3}$')]),
      jobStatus: new FormControl(null, [Validators.required]),
      bloodType: new FormControl(null, [Validators.required]),
      drRight: new FormControl(), //null, [Validators.required]),
      allergens: new FormControl(),
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
      photo: new FormControl(),
    });

    this.doctorService.getAllNonLoaded().subscribe((res) => {
      this.doctors = res;
    });

    this.allergensService.getAll().subscribe((res) => {
      this.allergens = res;
    });

    this.cityService.getAll().subscribe((res) => {
      this.cities = res;
    });
  }

  onSubmit(): void {
    this.uploadPicture().then((resultt) => {
      this.createPatient();
      this.registrationService.registerPatient(this.newPatient).subscribe(
        (res) => {
          this.router.navigate(['/']);
          this._snackBar.open(
            'Your registration request has been sumbitted. Please check your email and confirm your email adress to activate your account.',
            'Dismiss'
          );
        },
        (err) => {
          let parts = err.error.split(':');
          let mess = parts[parts.length - 1];
          let description = mess.substring(1, mess.length - 4);
          this._snackBar.open(description, 'Dismiss');
        }
      );
    });
  }

  onFileSelected(event: any): void {
    this.fileToUpload = <File>event.target.files[0];
    this.uploaded = true;
  }

  toBase64 = (file: Blob) =>
    new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });

  async uploadPicture() {
    if (this.uploaded) {
      await this.toBase64(this.fileToUpload).then(
        (res) => (this.newPatient.photoEncoded = res as string)
      );
    }
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

  getCity(event: any) {
    this.newPatient.cityId = event.value;
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
    this.newPatient.dateOfBirth = new Date(
      this.createForm.value.dateOfBirth.getTime() -
        this.createForm.value.dateOfBirth.getTimezoneOffset() * 60000
    );
    this.newPatient.medicalRecord.weight = this.createForm.value.weight;
    this.newPatient.medicalRecord.height = this.createForm.value.height;
    this.newPatient.phoneNumber = this.createForm.value.phoneNumber;
    this.newPatient.email = this.createForm.value.email;
    this.newPatient.userName = this.createForm.value.username;
    this.newPatient.password = this.createForm.value.password;
  }
}
