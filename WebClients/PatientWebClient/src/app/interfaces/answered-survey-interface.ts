import { IAnsweredQuestion } from 'src/app/interfaces/answered-question-interface';
export interface IAnsweredSurvey {

    surveyId: number,
    //PatientId: number,
    scheduledEventId: number,
    questions: IAnsweredQuestion[]

}