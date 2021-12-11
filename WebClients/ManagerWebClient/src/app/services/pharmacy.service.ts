import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IMedicineResponse } from "../interfaces/medicineResponse";


@Injectable()
export class PharmacyService{

    private _APIUrl = 'https://localhost:44302/api';  //uneti url za http get zahtev!
    
    constructor(private _httpClient: HttpClient) {}

    registerPharmacy(val:any){
        return this._httpClient.post(this._APIUrl +'/PharmacyCommunication/RegisterPharmacy',val);
    }

    getPharmacies(): Observable<any[]>{
        return this._httpClient.get<any[]>(this._APIUrl +'/Pharmacy/GetPharmacies');
    }

    getPharmacyById(id: number): Observable<any[]>{
        return this._httpClient.get<any[]>(this._APIUrl+`/Pharmacy/GetPharmacyById/${id}`);
    }

    updatePharmacy(val: any){
        return this._httpClient.post<any[]>(this._APIUrl + '/Pharmacy/UpdatePharmacy', val);
    }

    checkMedicine(val: any): Observable<IMedicineResponse>{
        return this._httpClient.post<IMedicineResponse>(this._APIUrl + '/Medicine/RequestMedicineInformation', val);
    }

    orderMedicine(val: any): Observable<IMedicineResponse>{
        return this._httpClient.post<IMedicineResponse>(this._APIUrl + '/Medicine/UrgentProcurementOfMedicine', val);
    }
}