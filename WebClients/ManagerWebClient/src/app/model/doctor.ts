import { Gender } from '../interfaces/patient';
import { DoctorSchedule } from './doctor-schedule.model';

export class Doctor {
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
  doctorScheduleId: number;
  doctorSchedule: DoctorSchedule;
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