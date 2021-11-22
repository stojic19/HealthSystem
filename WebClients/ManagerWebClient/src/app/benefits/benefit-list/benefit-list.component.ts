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
    this._benefitsService.getVisibleBenefits()
            .subscribe(benefits => this.benefits = benefits);
  }

}
