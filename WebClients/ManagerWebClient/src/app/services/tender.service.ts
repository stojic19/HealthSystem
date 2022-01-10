import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from "src/environments/environment";

@Injectable()
export class TenderService{

    private _APIUrl = `${environment.baseIntegrationUrl}`;
    
    constructor(private _httpClient: HttpClient) {}

    getTenders(): Observable<any[]>{
        return this._httpClient.get<any[]>(this._APIUrl + 'api/Tender/GetActiveTenders');
    }

    getTenderById(id: number): Observable<any>{
        return this._httpClient.get<any[]>(this._APIUrl + `api/Tender/GetTenderById/${id}`);
    }

    getTenderStatsForPharmacy(id: number): any{
        return this._httpClient.get<any>(this._APIUrl + `api/Tender/GetTenderStatsForPharmacy/${id}`);
    }

    postWinner(val: any){
        return this._httpClient.post<any[]>(this._APIUrl + 'api/Tender/ChooseWinningOffer', val);
    }

    closeTender(val: any): any{
        return this._httpClient.post<any>(this._APIUrl + 'api/Tender/CloseTender', val);
    }
    
}