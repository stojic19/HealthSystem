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
  doctor: any;
  bloodType: BloodType;
  allergies: any;
}
