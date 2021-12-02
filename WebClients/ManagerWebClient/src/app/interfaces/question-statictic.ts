export enum Category {
    HospitalSurvey, 
    DoctorSurvey, 
    StaffSurvey
  }
export interface IQuestionStatictic {
    id: number;
    text: string;
    category: Category;
    averageRating: number;
    ratingCounts: number[];
}

export class QuestionStatistic implements IQuestionStatictic{
    id: number ;
    text: string;
    category: Category;
    averageRating: number;
    ratingCounts: number[] = [];
    
  }