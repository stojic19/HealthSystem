import { ISurveySection } from "./isection";


export interface ISurvey {

    doctorSection: ISurveySection;
    medicalStaffSection: ISurveySection;
    hospitalSection: ISurveySection;
    surveyId: number;

}