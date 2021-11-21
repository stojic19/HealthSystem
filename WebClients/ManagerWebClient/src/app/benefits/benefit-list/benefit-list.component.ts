import { Component, OnInit } from '@angular/core';
import { BenefitsService } from 'src/app/services/benefits.service';

@Component({
  selector: 'app-benefit-list',
  templateUrl: './benefit-list.component.html',
  styleUrls: ['./benefit-list.component.css']
})
export class BenefitListComponent implements OnInit {

  constructor(private _benefitsService: BenefitsService) { }

  benefits: any[] = [];

  ngOnInit(): void {
    this.loadBenefits();
  }

  loadBenefits(): void {
    this._benefitsService.getVisibleBenefits()
            .subscribe(benefits => this.benefits = benefits);
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

}
