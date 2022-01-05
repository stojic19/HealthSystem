import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TenderService{

    private _APIUrl = 'https://localhost:44302/api';  //uneti url za http get zahtev!
    
    constructor(private _httpClient: HttpClient) {}

    getTenders(): Observable<any[]>{
        return this._httpClient.get<any[]>(this._APIUrl +'/Tender/GetTenders');
    }

    getTenderById(id: number): Observable<any>{
        return this._httpClient.get<any[]>(this._APIUrl+`/Tender/GetTenderById/${id}`);
    }
    
}