import { IMedicalRecord } from './medical-record';

export enum Gender {
  Female,
  Male,
}

export interface INewPatient {
  username: string;
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
  medicalRecord: IMedicalRecord;
}
