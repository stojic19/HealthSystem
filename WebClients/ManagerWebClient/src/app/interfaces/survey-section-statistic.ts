import { Category, IQuestionStatictic, QuestionStatistic } from "./question-statictic";

export interface ISurveySectionStatistic {   
    category: Category;
    questionsStatistic: IQuestionStatictic[] ;
    averageRating: number;
}
export class SurveySectionStatistic implements ISurveySectionStatistic{
    category: Category;
    questionsStatistic: QuestionStatistic[] = [new QuestionStatistic()];
    averageRating: number;
    
}
