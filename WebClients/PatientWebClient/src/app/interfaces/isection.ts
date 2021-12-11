import { IQuestion } from "./iquestion";
export enum SurveyCategory {
    HospitalSurvey,
    DoctorSurvey,
    StaffSurvey
}
export interface ISurveySection {

    questions: IQuestion[];
    category: SurveyCategory;
}
