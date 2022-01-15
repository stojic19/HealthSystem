import { DateSelectionModelChange } from "@angular/material/datepicker";

export enum VacationType {
    SickLeave,
    Vacation
  }

export class Vacation {
    type: VacationType;
    startDate: Date;
    endDate: Date;
}
