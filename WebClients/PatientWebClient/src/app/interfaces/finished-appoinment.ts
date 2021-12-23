import { IAppointment } from "./appointment";

export interface IFinishedAppointment{

    scheduledEventsDTO: IAppointment,
    answeredSurveyId: number
}