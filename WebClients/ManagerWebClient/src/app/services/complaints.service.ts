import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from "src/environments/environment";


@Injectable()
export class ComplaintsService{
    private _APIUrl = `${environment.baseUrl}`;  //uneti url za http get zahtev!
    
    constructor(private _httpClient: HttpClient) {}

    getComplaints(): Observable<any[]>{
        return this._httpClient.get<any[]>(this._APIUrl+"/Complaint/GetComplaints");
    }

    getComplaintById(id: number): Observable<any[]>{
        return this._httpClient.get<any[]>(this._APIUrl+`/Complaint/GetComplaintById/${id}`);
    }

    addComplaint(val:any){
        return this._httpClient.post(this._APIUrl+'/PharmacyCommunication/PostComplaint',val);
    }

    getPharmacies(): Observable<any[]>{
        return this._httpClient.get<any[]>(this._APIUrl+'/Pharmacy/GetPharmacies');
    }
}