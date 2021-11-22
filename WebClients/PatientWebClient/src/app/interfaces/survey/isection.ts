import { IQuestion } from "./iquestion";
export enum SurveyCategory {
    HospitalSurvey,
    DoctorSurvey,
    StaffSurvey
}
export interface ISurveySection {
    name: String;
    questions: IQuestion[];
    category: SurveyCategory;
}