import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TenderService } from '../services/tender.service';

@Component({
  selector: 'app-tenders-list',
  templateUrl: './tenders-list.component.html',
  styleUrls: ['./tenders-list.component.css']
})
export class TendersListComponent implements OnInit {
  tenders: any[] = [];

  constructor(private _tenderService: TenderService, private router: Router) { }

  ngOnInit(): void {
    //this._tenderService.getTenders()
     //       .subscribe(tenders => this.tenders = tenders);

  let tender = {
        id : 1,
        name: "tender 1",
        closedTime : new Date("1.1.2022."),
        active: true
      }

  this.tenders.push(tender);
  }

  doubleClickFunction(id: any){
    this.router.navigate(['/tender-profile', id])
  }
}
