import { SurveyCategory } from "./survey/isection";

export interface IAnsweredQuestion {
    // id: number,
    questionId: number,
    rating: number,
    type: SurveyCategory,
}