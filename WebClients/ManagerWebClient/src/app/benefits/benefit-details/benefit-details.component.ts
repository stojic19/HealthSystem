import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BenefitsService } from 'src/app/services/benefits.service';

@Component({
  selector: 'app-benefit-details',
  templateUrl: './benefit-details.component.html',
  styleUrls: ['./benefit-details.component.css']
})
export class BenefitDetailsComponent implements OnInit {

  constructor(private _route: ActivatedRoute, private _benefitsService: BenefitsService, private router:Router) { }

  benefit: any;
  id: number = -1;

  ngOnInit(): void {
    let id = Number(this._route.snapshot.paramMap.get('id'));
    this.id = id;

    this._benefitsService.getBenefitById(id)
            .subscribe(benefit => this.benefit = benefit);
  }

  publishBenefit(id: any) {
    var json = {benefitId: id}
    this._benefitsService.publishBenefit(json).subscribe(data => {console.log(data)});
    this.router.navigate(['/benefit-list']);
  }

  hideBenefit(id: any) {
    var json = {benefitId: id}
    this._benefitsService.hideBenefit(json).subscribe(data => {console.log(data)});
    this.router.navigate(['/benefit-list']);
  }

}
