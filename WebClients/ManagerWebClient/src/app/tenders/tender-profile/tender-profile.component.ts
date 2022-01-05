import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TenderService } from 'src/app/services/tender.service';

@Component({
  selector: 'app-tender-profile',
  templateUrl: './tender-profile.component.html',
  styleUrls: ['./tender-profile.component.css']
})
export class TenderProfileComponent implements OnInit {
  tender: any ={
    id : 1,
    name: "tender 1",
    closedTime : new Date("1.1.2022."),
    active: true,
    medicines: ["brufen", "andol"]
  };
  id: number = -1;
  constructor(private _route: ActivatedRoute, private _tenderService: TenderService) { }

  ngOnInit(): void {
    let id = Number(this._route.snapshot.paramMap.get('id'));
    this.id = id;

    //this._tenderService.getTenderById(id)
    //.subscribe(tender => {this.tender = tender},
    //(error) => alert(error.error));
  }

}
