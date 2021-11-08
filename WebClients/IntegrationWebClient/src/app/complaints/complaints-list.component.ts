import { Component, OnInit } from '@angular/core';
import { ComplaintsService } from './complaints.service';

@Component({
  selector: 'app-complaints-list',
  templateUrl: './complaints-list.component.html',
  styleUrls: ['./complaints-list.component.css']
})
export class ComplaintsListComponent implements OnInit {
  

  constructor(private _complaintsService: ComplaintsService) { }

  complaints: any = [];
  SearchString:string="";
  nonFilteredComplaints:any=[];

  ngOnInit(): void {

    this._complaintsService.getComplaints()
            .subscribe(complaints => this.complaints = complaints);
    this._complaintsService.getComplaints()
            .subscribe(complaints => this.nonFilteredComplaints = complaints);
  }

  FilterFn(){
    var searchString = this.SearchString.trim().toLowerCase();
    
    if(searchString == ""){
      this.complaints = this.nonFilteredComplaints;
      return;
    }

    this.complaints = [];
    for(var i = 0; i < this.nonFilteredComplaints.length; i++){
        if(this.nonFilteredComplaints[i].title.toLowerCase().includes(searchString) || this.nonFilteredComplaints[i].description.toLowerCase().includes(searchString)){
          this.complaints.push(this.nonFilteredComplaints[i]);
        }
    }
  }
}
