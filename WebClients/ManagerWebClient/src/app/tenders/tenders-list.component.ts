import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { TenderService } from '../services/tender.service';

@Component({
  selector: 'app-tenders-list',
  templateUrl: './tenders-list.component.html',
  styleUrls: ['./tenders-list.component.css']
})
export class TendersListComponent implements OnInit {
  tenders: any[] = [];
  isProd: boolean = environment.production;

  constructor(private _tenderService: TenderService, private router: Router) { }

  ngOnInit(): void {
    this._tenderService.getTenders()
            .subscribe(tenders => this.tenders = tenders);
  }

  doubleClickFunction(id: any){
    this.router.navigate(['/tender-profile', id])
  }
}
