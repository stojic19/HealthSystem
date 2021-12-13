import { Observable } from 'rxjs';
import { IChosenDoctor } from './chosen-doctor';
import { INewAllergy } from './new-allergy';

export enum BloodType {
  Undefined,
  ONegative,
  OPositive,
  ANegative,
  APositive,
  BNegative,
  BPositive,
  ABNegative,
  ABPositive,
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
  doctorId: number;
  doctor: IChosenDoctor;
  bloodType: BloodType;
  allergies: Array<INewAllergy>;
  jobStatus: JobStatus;
}
