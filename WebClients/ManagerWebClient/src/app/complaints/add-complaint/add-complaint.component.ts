import { Component, OnInit } from '@angular/core';
import { ComplaintsService } from 'src/app/services/complaints.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-complaint',
  templateUrl: './add-complaint.component.html',
  styleUrls: ['./add-complaint.component.css']
})
export class AddComplaintComponent implements OnInit {

  constructor(private _complaintsService: ComplaintsService, private toastr: ToastrService, private router:Router) { }

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
    this._complaintsService.addComplaint(val).subscribe(res =>
      {
        this.toastr.success(res.toString());
        this.router.navigate(['/complaints']);
      },(error) => console.log(error.error, "Error!"));
  }

  LoadPharmacyList(){
    this._complaintsService.getPharmacies().subscribe(data=>{
      this.PharmacyList=data;
    });
  }

}
