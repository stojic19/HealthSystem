import { Component, OnInit } from '@angular/core';
import { PharmacyService } from 'src/app/services/pharmacy.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-pharmacies-list',
  templateUrl: './pharmacies-list.component.html',
  styleUrls: ['./pharmacies-list.component.css']
})
export class PharmaciesListComponent implements OnInit {
  pharmacies: any[] = [];
  SearchString: string = "";
  MedicineString: string = "";
  ManufacturerString: string = "";
  QuantityString: string = "";
  nonFilteredPharmacies: any = [];
  confirmed: any = [];
  ordered: any = [];
  isProd: boolean = environment.production;

  constructor(private _pharmacyService: PharmacyService, private router: Router, private toastr: ToastrService) { }

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

  Clear(){
    this.confirmed = [];
    this.ordered = [];
  }

  doubleClickFunction(id: any){
    this.router.navigate(['/pharmacy-profile', id])
  }

  PharmacyConfirmed(id: any){
    return this.confirmed.indexOf(id)!=-1;
  }
  PharmacyOrdered(id: any){
    return this.ordered.indexOf(id)!=-1;
  }

  Check(id: any){
    if(this.MedicineString != "" && this.QuantityString !="" && this.ManufacturerString != ""){
      this._pharmacyService.checkMedicine(this.MakeRequest(id))
      .subscribe(res => {
        if(res.answer == true ){
          this.confirmed.push(id);
        }},
        (error) => this.toastr.error(error.error)
        );
    }
  }

  Order(id: any){
    if(this.MedicineString != "" && this.QuantityString !="" && this.ManufacturerString != ""){
      this._pharmacyService.orderMedicine(this.MakeRequest(id))
      .subscribe(res => {
        if(res.answer == true){
          this.ordered.push(id);
        }},
        (error) => this.toastr.error(error.error)
        );
    }
  }

  MakeRequest(id: any){
    var medicineReq = {
      PharmacyId : id,
      MedicineName : this.MedicineString,
      ManufacturerName : this.ManufacturerString, 
      Quantity : parseInt(this.QuantityString)
    }
    return medicineReq;
  }

}
