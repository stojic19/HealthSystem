import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IMedicationIngredient } from 'src/app/interfaces/medication-ingredient';

@Injectable({
  providedIn: 'root',
})
export class AllergensService {
  constructor(private _http: HttpClient) {}

  getAll(): Observable<IMedicationIngredient[]> {
    return this._http.get<IMedicationIngredient[]>('/api/Allergen');
  }
}
