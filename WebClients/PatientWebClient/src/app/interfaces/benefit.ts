import { SafeStyle } from "@angular/platform-browser";

export interface IBenefit {
    pharmacyName: string;
    title: string;
    description: string;
    startTime: Date;
    endTime: Date;
    picture: string;
    imageSrc: SafeStyle;
    pharmacyId : number;
}
