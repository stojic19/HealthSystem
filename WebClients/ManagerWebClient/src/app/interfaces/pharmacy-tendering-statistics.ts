import { IMoney } from "./money";

export interface IPharmacyTenderingStatistics {
    pharmacyId : number;
    pharmacyName : string;
    tendersEntered : number;
    tendersWon : number;
    tenderOffersMade : number;
    profit : IMoney;
}