import { IMedicalRecord } from './medical-record';

export enum Gender {
  Male,
  Female,
}

export interface INewPatient {
  userName: string;
  password: string;
  firstName: string;
  middleName: string;
  lastName: string;
  dateOfBirth: Date;
  gender: Gender;
  email: string;
  phoneNumber: string;
  street: string;
  streetNumber: string;
  cityId: number; // create city??
  //medicalRecordId: number;
  medicalRecord: IMedicalRecord;
}