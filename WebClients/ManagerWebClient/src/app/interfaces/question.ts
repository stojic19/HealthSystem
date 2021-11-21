export enum Category {
    Doctor, 
    MedicalStaff, 
    Hospital
  }

export interface IQuestion {
    id: number;
    text: string;
    ratings: number[];
    category: Category;
    avgRating: number;
    numOfEachRating: number[];
}
