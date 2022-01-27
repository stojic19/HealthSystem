import { Vacation } from "./doctor";
import { OnCallDuty } from "./on-call-duty";

export class DoctorSchedule {
    id: number;
    onCallDuties: OnCallDuty[];
    vacations: Vacation[];
}
