import { Component, OnInit } from '@angular/core';
import { PharmacyService } from '../pharmacy/pharmacy.service';

@Component({
  selector: 'app-pharmacy-list',
  templateUrl: './pharmacy-list.component.html',
  styleUrls: ['./pharmacy-list.component.css']
})
export class PharmacyListComponent implements OnInit {
  pharmacies: any[] = [];

  constructor(private _pharmacyService: PharmacyService) { }

  ngOnInit(): void {
    this._pharmacyService.getPharmacies()
            .subscribe(pharmacies => this.pharmacies = pharmacies);
  }

}
