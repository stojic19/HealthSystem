import { Component, OnInit } from '@angular/core';
import { ComplaintsService } from 'src/app/services/complaints.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-complaints-list',
  templateUrl: './complaints-list.component.html',
  styleUrls: ['./complaints-list.component.css']
})
export class ComplaintsListComponent implements OnInit {
  
    isProd: boolean = environment.production;

  constructor(private _complaintsService: ComplaintsService) { }

  complaints: any = [];
  SearchString:string="";
  nonFilteredComplaints:any=[];
  isAnswered:string="All";

  ngOnInit(): void {

    this._complaintsService.getComplaints()
            .subscribe(complaints => this.complaints = complaints);
    this._complaintsService.getComplaints()
            .subscribe(complaints => this.nonFilteredComplaints = complaints);
  }

  FilterFn(){
    var searchString = this.SearchString.trim().toLowerCase();
    
    this.complaints = this.nonFilteredComplaints.map((complaint: any) => complaint);
    if(!(searchString === "")){
      this.complaints = [];
      for(var i = 0; i < this.nonFilteredComplaints.length; i++){
        if(this.nonFilteredComplaints[i].title.toLowerCase().includes(searchString) 
        || this.nonFilteredComplaints[i].description.toLowerCase().includes(searchString)){
          this.complaints.push(this.nonFilteredComplaints[i]);
        }
      }
    }

    if(!(this.isAnswered === "All")){
      for(var i = 0; i < this.complaints.length; i++){
          if(!((this.isAnswered === "Answered" && this.complaints[i].complaintResponse != null) || (this.isAnswered === "Not answered" && this.complaints[i].complaintResponse == null)) ){
            this.complaints.splice(i, 1);
            i--;
          }
      }
    }
  }
}