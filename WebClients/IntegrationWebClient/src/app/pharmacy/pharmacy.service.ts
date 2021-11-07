import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable()
export class PharmacyService{

    private _APIUrl = 'http://localhost:5000/api';  //uneti url za http get zahtev!
    
    constructor(private _httpClient: HttpClient) {}

    registerPharmacy(val:any){
        return this._httpClient.post(this._APIUrl+'/PharmacyCommunication/RegisterPharmacy',val);
    }
}