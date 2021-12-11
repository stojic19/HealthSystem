import { IAnsweredQuestion } from 'src/app/interfaces/answered-question';
export interface IAnsweredSurvey {

    surveyId: number,
    //PatientId: number,
    scheduledEventId: number,
    questions: IAnsweredQuestion[]

}