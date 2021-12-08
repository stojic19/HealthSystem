import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

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

  openPdf(fileName: string): void
  {
    this._httpClient.post(this._APIUrl+'/MedicineSpecification/getSpecificationPdf?fileName=' + fileName, {location: "report.pdf"}, { responseType: 'blob' }).subscribe(
      (response) => {
          var blob = new Blob([response], { type: "application/pdf" });
          window.open(URL.createObjectURL(blob), "_blank")
      },
      e => { throwError(e); }
    );
  }

  sendRequest(val:any)
  {
    return this._httpClient.post(this._APIUrl+'/MedicineSpecification/SendMedicineSpecificationRequest',val);
  }
}
