import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TimeService {

  times : string[]=[];
  constructor() { 
    this.times.push("07:00");
    this.times.push("08:00");
    this.times.push("09:00");
    this.times.push("10:00");
    this.times.push("11:00");
    this.times.push("12:00");
    this.times.push("13:00");
    this.times.push("14:00");
    this.times.push("15:00");
    this.times.push("16:00");
    this.times.push("17:00");
    this.times.push("18:00");
    this.times.push("19:00");
  }
}
