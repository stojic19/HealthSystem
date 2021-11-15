import { IPatient } from "./patient";


export enum FeedbackStatus {
    Pending, 
    Rejected, 
    Approved
  }

export interface IFeedback {
    id: number;
    patientId: number;
    patient: IPatient;
    feedbackStatus: FeedbackStatus;
    createdDate: Date;
    text: string;
    isPublishable: boolean;
        
}

