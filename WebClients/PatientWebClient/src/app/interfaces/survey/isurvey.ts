import { ISurveySection } from "./isection";

export interface ISurvey {
    id: number;
    doctorSection: ISurveySection;
    medicalStaffSection: ISurveySection;
    hospitalSection: ISurveySection;
}