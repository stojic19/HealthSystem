import { SurveyCategory } from "./isection";

export interface IAnsweredQuestion {
    // id: number,
    questionId: number,
    rating: number,
    type: SurveyCategory,
}