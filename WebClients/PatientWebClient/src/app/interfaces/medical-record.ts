import { IChosenDoctor } from './chosen-doctor';

export enum BloodType {
  Undefined = 'Undefined',
  ONegative = '0 Negative',
  OPositive = '0 Positive',
  ANegative = 'A Negative',
  APositive = 'A Positive',
  BNegative = 'B Negative',
  BPositive = 'B Positive',
  ABNegative = 'AB Negative',
  ABPositive = 'AB Positive',
}

export enum JobStatus {
  Undefined,
  Employed,
  Unemployed,
  Student,
  Retired,
  Child,
}

export interface IMedicalRecord {
  height: number;
  weight: number;
  doctor: IChosenDoctor;
  bloodType: BloodType;
  allergies: any;
  jobStatus: JobStatus;
}
