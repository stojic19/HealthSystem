import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MedicineSpecificationRequestsService {
  private _APIUrl = 'http://localhost:5000/api';
  constructor(private _httpClient: HttpClient) { }
  
  getPharmacies(): Observable<any[]>
  {
    return this._httpClient.get<any[]>(this._APIUrl+'/Pharmacy/GetPharmacies');
  }

  getAllMedicineSpecificationFiles(): Observable<any[]>
  {
    return this._httpClient.get<any[]>(this._APIUrl+'/MedicineSpecification/GetAllMedicineSpecificationFiles');
  }

  getSpecificationPdf(fileName: string): any
  {
    return this._httpClient.get(this._APIUrl+'/MedicineSpecification/getSpecificationPdf?fileName=' + fileName)
  }

  sendRequest(val:any)
  {
    return this._httpClient.post(this._APIUrl+'/MedicineSpecification/SendMedicineSpecificationRequest',val);
  }
}
