import { IChosenDoctor } from './chosen-doctor';
import { IMedication } from './imedication';
import { IPatient } from './patient-interface';

export interface IPrescription {
  issuedDate: string;
  endDate: string;
  startDate: string;
  medication: IMedication;
  doctorInfo: IChosenDoctor;
  patientInfo: IPatient;
}
