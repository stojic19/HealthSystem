import { analyzeFileForInjectables } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { BenefitsService } from 'src/app/services/benefits.service';
import { PharmacyService } from 'src/app/services/pharmacy.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-benefit-list',
  templateUrl: './benefit-list.component.html',
  styleUrls: ['./benefit-list.component.css']
})
export class BenefitListComponent implements OnInit {

    isProd: boolean = environment.production;

  constructor(private _benefitsService: BenefitsService, private _pharmaciesService: PharmacyService) { 
    this._pharmaciesService.getPharmacies()
            .subscribe(pharmacies => this.filterPharmacies = pharmacies);
    
    this.SearchPharmacy = {
      "pharmacy": {
        "_id": "0",
        "title": "All pharmacies"
      }
    }

    this.allPharmacies = {
      "id": "0",
      "name": "All pharmacies"
    }
    this.SearchPharmacy.pharmacy = this.allPharmacies
  }

  benefits: any[] = [];
  SearchString:string="";
  nonFilteredBenefits:any=[];
  onlyActive:boolean=false;
  filterPharmacies:Array<any>;
  SearchPharmacy:any;
  allPharmacies:any;

  ngOnInit(): void {
    this.loadBenefits();
  }

  loadBenefits(): void {
    this._benefitsService.getVisibleBenefits()
            .subscribe(benefits => this.benefits = benefits);
    this._benefitsService.getVisibleBenefits()
            .subscribe(benefits => this.nonFilteredBenefits = benefits);
    this._pharmaciesService.getPharmacies()
            .subscribe(pharmacies => this.filterPharmacies = pharmacies);
  }

  publishBenefit(id: any) {
    var json = {benefitId: id}
    this._benefitsService.publishBenefit(json).subscribe(data => {console.log(data)});
    this.loadBenefits();
  }

  hideBenefit(id: any) {
    var json = {benefitId: id}
    this._benefitsService.hideBenefit(json).subscribe(data => {console.log(data)});
    this.loadBenefits();
  }

  FilterFn(){
    var searchString = this.SearchString.trim().toLowerCase();
    
    this.benefits = this.nonFilteredBenefits.map((benefit: any) => benefit);
    if(!(searchString === "")){
      this.benefits = [];
      for(var i = 0; i < this.nonFilteredBenefits.length; i++){
        if(this.nonFilteredBenefits[i].title.toLowerCase().includes(searchString) 
        || this.nonFilteredBenefits[i].description.toLowerCase().includes(searchString)){
          this.benefits.push(this.nonFilteredBenefits[i]);
        }
      }
    }

    if(this.onlyActive){
      for(var i = 0; i < this.benefits.length; i++){
        if(new Date(this.benefits[i].startTime).getTime() > new Date().getTime()
        || new Date(this.benefits[i].endTime).getTime() < new Date().getTime()){
            this.benefits.splice(i, 1);
            i--;
        }
      }
    }

    if(!(this.SearchPharmacy.pharmacy.id === "0")){
      const pharmacyId = Number.parseInt(this.SearchPharmacy.pharmacy.id)
      for(var i = 0; i < this.benefits.length; i++){
          if(pharmacyId != this.benefits[i].pharmacy.id){
            this.benefits.splice(i, 1);
            i--;
          }
      }
    }
  }

  compareObjects(o1: any, o2: any): boolean {
    return o1.name === o2.name && o1._id === o2._id;
  }

}
