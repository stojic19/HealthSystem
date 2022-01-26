export enum Step
{
    StartScheduling,
    DateNext,
    SpecializationNext,
    SpecializationBack,
    DoctorNext,
    DoctorBack,
    AppointmentBack,
    Schedule
}

export interface IEvent {
    username : string;
    step: Step;
}

