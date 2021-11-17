import { Component, OnInit } from '@angular/core';
import { PharmacyService } from 'src/app/services/pharmacy.service';

@Component({
  selector: 'app-pharmacies-list',
  templateUrl: './pharmacies-list.component.html',
  styleUrls: ['./pharmacies-list.component.css']
})
export class PharmaciesListComponent implements OnInit {
  pharmacies: any[] = [];

  constructor(private _pharmacyService: PharmacyService) { }

  SearchString:string="";
  nonFilteredPharmacies:any=[];

  ngOnInit(): void {
    this._pharmacyService.getPharmacies()
            .subscribe(pharmacies => this.pharmacies = pharmacies);
    this._pharmacyService.getPharmacies()
            .subscribe(pharmacies => this.nonFilteredPharmacies = pharmacies);
  }

  FilterFn(){
    var searchString = this.SearchString.trim().toLowerCase();
    
    if(searchString == ""){
      this.pharmacies = this.nonFilteredPharmacies;
      return;
    }

    this.pharmacies = [];
    for(var i = 0; i < this.nonFilteredPharmacies.length; i++){
        if(this.nonFilteredPharmacies[i].name.toLowerCase().includes(searchString) ||
        (this.nonFilteredPharmacies[i].streetName + " " + this.nonFilteredPharmacies[i].streetNumber + " " + 
        this.nonFilteredPharmacies[i].city.name + " " + this.nonFilteredPharmacies[i].city.country.name).toLowerCase().includes(searchString)){
          this.pharmacies.push(this.nonFilteredPharmacies[i]);
        }
    }
  }

}
