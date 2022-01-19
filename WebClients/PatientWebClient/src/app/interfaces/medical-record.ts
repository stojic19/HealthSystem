import { IChosenDoctor } from './chosen-doctor';
import { IMeasurements } from './imeasurements';
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
  measurements: IMeasurements;
  doctorId: number;
  doctor: IChosenDoctor;
  bloodType: BloodType;
  allergies: Array<INewAllergy>;
  jobStatus: JobStatus;
}
