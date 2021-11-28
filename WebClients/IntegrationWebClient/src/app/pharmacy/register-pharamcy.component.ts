import { Component, OnInit } from '@angular/core';
import { PharmacyService } from './pharmacy.service';

@Component({
  selector: 'app-register-pharamcy',
  templateUrl: './register-pharamcy.component.html',
  styleUrls: ['./register-pharamcy.component.css']
})
export class RegisterPharamcyComponent implements OnInit {

  constructor(private _PharmacyService: PharmacyService) { }

  name:string="";
  streetNumber:string="";
  streetName:string="";
  cityName:string="";
  postalCode:string="";
  country:string="";
  baseUrl:string="";

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
              baseUrl:this.baseUrl
    }
    this._PharmacyService.registerPharmacy(val).subscribe(res=>{alert(res)});
  }

}
