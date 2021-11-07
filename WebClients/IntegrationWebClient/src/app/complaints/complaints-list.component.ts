import { Component, OnInit } from '@angular/core';
import { ComplaintsService } from './complaints.service';

@Component({
  selector: 'app-complaints-list',
  templateUrl: './complaints-list.component.html',
  styleUrls: ['./complaints-list.component.css']
})
export class ComplaintsListComponent implements OnInit {
  complaints: any[] = [];



  constructor(private _complaintsService: ComplaintsService) { }

  ngOnInit(): void {

    this._complaintsService.getComplaints()
            .subscribe(complaints => this.complaints = complaints);
  }

}
