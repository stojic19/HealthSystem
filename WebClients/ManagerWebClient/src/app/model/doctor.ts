import { Gender } from '../interfaces/patient';

export interface Doctor {
  id: number;
  username: string;
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
  city: any;
  specializationId: number;
  specialization: any;
  shiftId: number;
  shift: Shift;
  onCallDuties: OnCallDuty[];
  vacations: Vacation[];
}

export interface Shift {
  id: number;
  name: string;
  from: number;
  to: number
}

export interface OnCallDuty {
  id: number;
  month: number;
  week: number;
  doctorsOnDuty: Doctor[]
}

export interface Vacation {
  id: number;
  type: VacationType;
  startDate: Date;
  endDate: Date;
}

export enum VacationType {
  SickLeave,
  Vacation
}