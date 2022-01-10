import { VacationType } from "./vacation.model";

export class VacationRequest {
    type: VacationType;
    startDate: Date;
    endDate: Date;
    doctorId: number;
}