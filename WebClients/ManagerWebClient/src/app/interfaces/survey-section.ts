import { IQuestion } from "./question";

export interface ISurveySection {
    name:String;
    questions: IQuestion[];
    avgRating: number;
}
