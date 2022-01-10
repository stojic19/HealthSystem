import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TenderService } from 'src/app/services/tender.service';

@Component({
  selector: 'app-tender-profile',
  templateUrl: './tender-profile.component.html',
  styleUrls: ['./tender-profile.component.css']
})
export class TenderProfileComponent implements OnInit {
  tender: any;
  id: number = -1;

  constructor(private _route: ActivatedRoute, private _tenderService: TenderService) { }

  ngOnInit(): void {
    let id = Number(this._route.snapshot.paramMap.get('id'));
    this.id = id;

    this._tenderService.getTenderById(id)
    .subscribe(tender => {this.tender = tender},
    (error) => alert(error.error));
  }

  winner(offerId: number){
    var winner={
      tenderId : this.id,
      offerId: offerId
    }
    this._tenderService.postWinner(winner)
    .subscribe((error) => alert(error));
  }

  closeTender(){
    var val = {
      tenderId: this.id
    }
    this._tenderService.closeTender(val)
    .subscribe((error) => alert(error));
  }

}
