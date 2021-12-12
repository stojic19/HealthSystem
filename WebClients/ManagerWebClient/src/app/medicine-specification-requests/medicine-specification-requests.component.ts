import { Component, OnInit } from '@angular/core';
import { MedicineSpecificationRequestsService } from 'src/app/services/medicine-specification-requests.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-medicine-specification-requests',
  templateUrl: './medicine-specification-requests.component.html',
  styleUrls: ['./medicine-specification-requests.component.css']
})
export class MedicineSpecificationRequestsComponent implements OnInit {

  constructor(private _medicineRequestsService: MedicineSpecificationRequestsService, private toastr: ToastrService) { }
  medicineName: any;
  Pharmacy: any;
  PharmacyList: any[] = [];
  ngOnInit(): void {
    this.LoadPharmacyList();
  }
  LoadPharmacyList(){
    this._medicineRequestsService.getPharmacies().subscribe(data=>{
      this.PharmacyList=data;
      this.Pharmacy = this.PharmacyList[0];
    });
  }
  SendRequest(){
    if(this.Pharmacy == null || this.medicineName === "" || this.medicineName == null)
    {
      this.toastr.error("Enter valid pharmacy and medicine name!", "Error!");
      return;
    }
    var request = 
    {
      medicineName: this.medicineName,
      pharmacyId: this.Pharmacy.id
    }
    this._medicineRequestsService.sendRequest(request).subscribe(res =>
      {
        this.toastr.success(res.toString());
      },(error) => this.toastr.error(error.error));
  }
}