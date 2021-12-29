import { ICity } from './city';
import { IMedicalRecord } from './medical-record';

export enum Gender {
  Male,
  Female,
}

export interface IPatient {
  id: number;
  userName: string;
  middleName: string;
  password: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  gender: Gender;
  email: string;
  phoneNumber: string;
  street: string;
  streetNumber: string;
  cityId: number;
  city: ICity;
  medicalRecord: IMedicalRecord;
  photoEncoded: string;
}
