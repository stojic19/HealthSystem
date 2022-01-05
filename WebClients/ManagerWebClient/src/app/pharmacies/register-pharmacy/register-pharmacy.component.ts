import { Component, OnInit } from '@angular/core';
import { PharmacyService } from 'src/app/services/pharmacy.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register-pharmacy',
  templateUrl: './register-pharmacy.component.html',
  styleUrls: ['./register-pharmacy.component.css']
})
export class RegisterPharmacyComponent implements OnInit {

  constructor(private _PharmacyService: PharmacyService, private toastr: ToastrService) { }

  name:string="";
  streetNumber:string="";
  streetName:string="";
  cityName:string="";
  postalCode:string="";
  country:string="";
  baseUrl:string="";
  grpcSupported:boolean=false;

  ngOnInit(): void {
  }

  registerPharmacy(){
    var val = {name:this.name,
              streetNumber:this.streetNumber,
              streetName:this.streetName,
              city:{
                name:this.cityName,
                postalCode:this.postalCode,
                country:{
                  name:this.country
                }
              },
              baseUrl:this.baseUrl,
              grpcSupported:this.grpcSupported
    }
    this._PharmacyService.registerPharmacy(val).subscribe(res => console.log(res),
     (error) => this.toastr.error(error.message));
  }

}
