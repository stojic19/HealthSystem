import { IPatient } from "./patient";


export enum FeedbackStatus {
    pending, 
    rejected, 
    approved
  }

export interface IFeedback {
    id: number;
    patientId: string;
    patient: IPatient;
    feedbackStatus: FeedbackStatus;
    createdDate: Date;
    text: string;
    isPublishable: boolean;
        
}

