import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class MedicineSpecificationRequestsService {
  private _APIUrl = `${environment.baseIntegrationUrl}`;
  constructor(private _httpClient: HttpClient) { }
  
  getPharmacies(): Observable<any[]>
  {
    return this._httpClient.get<any[]>(this._APIUrl+'api/Pharmacy/GetPharmacies');
  }

  getAllMedicineSpecificationFiles(): Observable<any[]>
  {
    return this._httpClient.get<any[]>(this._APIUrl+'api/MedicineSpecification/GetAllMedicineSpecificationFiles');
  }

  openPdf(fileId: number): void
  {
    this._httpClient.post(this._APIUrl+'api/MedicineSpecification/getSpecificationPdf?fileId=' + fileId, {location: "report.pdf"}, { responseType: 'blob' }).subscribe(
      (response) => {
          var blob = new Blob([response], { type: "application/pdf" });
          window.open(URL.createObjectURL(blob), "_blank")
      },
      e => { throwError(e); }
    );
  }

  sendRequest(val:any)
  {
    return this._httpClient.post(this._APIUrl+'api/MedicineSpecification/SendMedicineSpecificationRequest',val);
  }
}
