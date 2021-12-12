import { IPatient } from '../interfaces/patient';
import { Room } from '../interfaces/room';
import { Doctor } from './doctor';

export enum ScheduledEventType {
  Appointment,
  Operation,
  Renovation,
}

export class Schedule {
  id: number;
  scheduledEventType: ScheduledEventType;
  isCanceled: boolean;
  isDone: boolean;
  startDate: Date;
  endDate: Date;
  patient: IPatient;
  doctor: Doctor;
  roomId: number;
  room: Room;
}
