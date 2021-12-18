
export interface IAppointment{
    id: number,
    isDone: boolean,
    startDate: Date,
    endDate: Date,
    doctorName : String,
    doctorLastName: String,
    specializationName : String,
    roomName : String
}