import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category, IQuestion } from '../interfaces/question';
import { ISurvey } from '../interfaces/survey';

@Injectable({
  providedIn: 'root'
})
export class SurveyObserveService {

  survey: ISurvey = { id: 1, name: 'Hospital survay', createdDate: new Date(), sectionsAvg: [3.54, 3.98, 4.1], active: true, 
  categoryHospital: { name: 'Hospital', avgRating: 3.54, 
      questions: [{ id: 1, text: 'How did you find the experience of booking appointments?', ratings: [2, 3, 5, 2, 3], category: Category.Hospital, avgRating: 3.7, numOfEachRating: [3, 2, 1, 2, 3] }, { id: 2, text: 'How easy is it to navigate our application?', ratings: [2, 3, 5, 2], category: Category.Hospital, avgRating: 2.7, numOfEachRating: [3, 2, 1] },{ id: 3, text: 'Were we able to answer all your questions?', ratings: [2, 3, 5, 2], category: Category.Hospital, avgRating: 3.5, numOfEachRating: [3, 2, 1] },{ id: 4, text: 'How likely are you to recommend us to your friends and family?', ratings: [2, 3, 5, 2], category: Category.Hospital, avgRating: 4.2, numOfEachRating: [3, 2, 1] },{ id: 5, text: 'What is your overall satisfaction with application?', ratings: [2, 3, 5, 2], category: Category.Hospital, avgRating: 4.6, numOfEachRating: [3, 2, 1] }] }, 
                categoryDoctor: { name: 'Doctor', avgRating: 3.98,
      questions: [{ id: 6, text: 'How long did you have to wait until the doctor attends to you?', ratings: [2, 3, 5, 2, 3], category: Category.Doctor, avgRating: 3.7, numOfEachRating: [3, 2, 1, 6, 8] }, { id: 7, text: 'Were you satisfied with the doctor you were allocated with?', ratings: [2, 3, 5, 2], category: Category.Doctor, avgRating: 3.7, numOfEachRating: [3, 2, 1] },{ id: 8, text: 'How happy are you with the doctor’s treatment??', ratings: [2, 3, 5, 2, 3], category: Category.Doctor, avgRating: 3.7, numOfEachRating: [3, 2, 1, 6, 8] },{ id: 9, text: 'How would you rate the professionalism of doctor?', ratings: [2, 3, 5, 2, 3], category: Category.Doctor, avgRating: 3.7, numOfEachRating: [3, 2, 1, 6, 8] },{ id: 10, text: 'What is your overall satisfaction with doctor?', ratings: [2, 3, 5, 2, 3], category: Category.Doctor, avgRating: 3.7, numOfEachRating: [3, 2, 1, 6, 8] }] }, 
                 categoryStaff: { name: 'MedicalStaff', avgRating: 4.1, 
      questions: [{ id: 11, text: 'Were our staff empathetic to your needs?', ratings: [2, 3, 5, 2, 3], category: Category.MedicalStaff, avgRating: 3.7, numOfEachRating: [3, 2, 1, 7, 8] }, { id: 12, text: 'How would you rate the professionalism of our staff?', ratings: [2, 3, 5, 2], category: Category.Doctor, avgRating: 3.7, numOfEachRating: [3, 2, 1] },{ id: 13, text: 'Were the staff quick to respond to your medical care request?', ratings: [2, 3, 5, 2, 3], category: Category.MedicalStaff, avgRating: 3.7, numOfEachRating: [3, 2, 1, 7, 8] },{ id: 14, text: 'How would you rate courtesy of our staff?', ratings: [2, 3, 5, 2, 3], category: Category.MedicalStaff, avgRating: 3.7, numOfEachRating: [3, 2, 1, 7, 8] },{ id: 15, text: 'What is your overall satisfaction with staff?', ratings: [2, 3, 5, 2, 3], category: Category.MedicalStaff, avgRating: 3.7, numOfEachRating: [3, 2, 1, 7, 8] }] }}


  constructor(private http: HttpClient) { }

  getSurvey(): ISurvey {

    return this.survey;
  }
}
