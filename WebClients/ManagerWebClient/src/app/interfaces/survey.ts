import { ISurveySection } from "./survey-section";

export interface ISurvey {
    id: number;
    name: string;
    createdDate: Date;
    categoryHospital: ISurveySection;
    categoryDoctor: ISurveySection;
    categoryStaff: ISurveySection;
    sectionsAvg: number[];
    active: boolean;
}
