import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ComplaintsService } from 'src/app/services/complaints.service';

@Component({
  selector: 'app-complaint-details',
  templateUrl: './complaint-details.component.html',
  styleUrls: ['./complaint-details.component.css']
})
export class ComplaintDetailsComponent implements OnInit {
  complaint: any;
  id: number = -1;
  description = "opis zalbe";

  constructor(private _route: ActivatedRoute, private _complaintsService: ComplaintsService) { }

  ngOnInit(): void {
    let id = Number(this._route.snapshot.paramMap.get('id'));
    this.id = id;

    this._complaintsService.getComplaintById(id)
            .subscribe(complaint => this.complaint = complaint);
  }

}