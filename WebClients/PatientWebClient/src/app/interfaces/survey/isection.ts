import { IQuestion } from "./iquestion";

export interface ISurveySection {
    name: String;
    questions: IQuestion[];
}