import { DoctorSchedule } from "./doctor-schedule.model";
import { Vacation, VacationType } from "./vacation.model";

export class DoctorVacation {
    id: number;
    firstName: string;
    lastName: string;
    doctorSchedule: DoctorSchedule;
}
