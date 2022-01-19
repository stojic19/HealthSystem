export interface IFeedback {
  patientUsername: string;
  text: string;
  isPublishable: boolean;
  isAnonymous: boolean;
  patientId: number;
}
