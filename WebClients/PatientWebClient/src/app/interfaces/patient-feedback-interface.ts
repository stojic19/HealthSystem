import { IPatient } from './patient-interface';

export enum FeedbackStatus {
    Pending,
    Rejected,
    Approved
}
export interface IPatientFeedback {

    Id: number;
    PatientId: number;
    Patient: IPatient;
    FeedbackStatus: FeedbackStatus;
    CreateDate: Date;
    Text: String;
    IsPublishable: Boolean;



}