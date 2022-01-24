import { IAnsweredQuestion } from 'src/app/interfaces/answered-question';
export interface IAnsweredSurvey {

    surveyId: number,
    userName:  String,
    scheduledEventId: number,
    questions: IAnsweredQuestion[]

}