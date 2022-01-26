import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable()
export class NotificationsService{

    private _APIUrl = `${environment.baseIntegrationUrl}`;
    
    constructor(private _httpClient: HttpClient) {}

    getNotifications(): Observable<any[]>{
        return this._httpClient.get<any[]>(this._APIUrl + 'api/Notification/GetNotifications');
    }

    seen(id: number){
        return this._httpClient.get<any[]>(this._APIUrl + `api/Notification/Seen/${id}`);
    }

}