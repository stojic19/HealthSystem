import { Component, OnInit } from '@angular/core';
import { ComplaintsService } from 'src/app/services/complaints.service';

@Component({
  selector: 'app-add-complaint',
  templateUrl: './add-complaint.component.html',
  styleUrls: ['./add-complaint.component.css']
})
export class AddComplaintComponent implements OnInit {

  constructor(private _complaintsService: ComplaintsService) { }

  PharmacyList: any[] = [];

  title:string="";
  description:string="";
  pharmacyName:string="";
  pharmacyId:string="";
  Pharmacy:any={};

  ngOnInit(): void {
    this.LoadPharmacyList();
  }

  addComplaint(){
    var val = {title:this.title,
              description:this.description,
              pharmacyName:this.Pharmacy.name,
              pharmacyId:this.Pharmacy.id
    }
    this._complaintsService.addComplaint(val).subscribe(res=>{alert(res)});
  }

  LoadPharmacyList(){
    this._complaintsService.getPharmacies().subscribe(data=>{
      this.PharmacyList=data;
    });
  }

}
