import { Component, OnInit } from '@angular/core';
import { PharmacyService } from 'src/app/services/pharmacy.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-pharmacy',
  templateUrl: './register-pharmacy.component.html',
  styleUrls: ['./register-pharmacy.component.css']
})
export class RegisterPharmacyComponent implements OnInit {

  constructor(private _PharmacyService: PharmacyService, private toastr: ToastrService, private router:Router) { }

  baseUrl:string="";

  ngOnInit(): void {
  }

  registerPharmacy(){
    if(this.baseUrl === ""){
      this.toastr.error("Please enter url!");
      return;
    }
    var val = {
              baseUrl:this.baseUrl,
    }
    this._PharmacyService.registerPharmacy(val).subscribe(res =>
      {
        this.toastr.success(res.toString());
        this.router.navigate(['/pharmacy-list']);
      },(error) => this.toastr.error(error.error, "Error!"));
  }
}
