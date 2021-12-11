import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PharmacyService } from 'src/app/services/pharmacy.service';

@Component({
  selector: 'app-pharmacy-profile',
  templateUrl: './pharmacy-profile.component.html',
  styleUrls: ['./pharmacy-profile.component.css']
})
export class PharmacyProfileComponent implements OnInit {
  pharmacy: any;
  id: number = -1;
  imageSrc: string = "./assets/images/no-image.jpg";
  imageFile: File;

  constructor(private _route: ActivatedRoute, private _pharmacyService: PharmacyService) { }

  ngOnInit(): void {
    let id = Number(this._route.snapshot.paramMap.get('id'));
    this.id = id;

    this._pharmacyService.getPharmacyById(id)
    .subscribe(pharmacies => {this.pharmacy = pharmacies},
      (error) => alert(error.error));
  }

  onFileChange(event) {
    const reader = new FileReader();
    
    if(event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {this.imageSrc = reader.result as string};
      this.imageFile = file;
    }
  }

  UpdatePharmacy(){
    this._pharmacyService.updatePharmacy(this.MakeRequest())
    .subscribe(res => {},
      (error) => alert(error.error)
      );
  }

  MakeRequest(){
    var updatedPharmacy = {           //napraviti DTO na backu
      PharmacyId : this.pharmacy.id,
      PharmacyName: this.pharmacy.name,
      StreetNumber: this.pharmacy.streetNumber,
      StreetName: this.pharmacy.streetName,
      CityName: this.pharmacy.city.name,
      PostalCode: this.pharmacy.city.postalCode,
      Country: this.pharmacy.city.country.name,
      Description: this.pharmacy.description,
      Image: this.imageFile
    }
    return updatedPharmacy;
  }

}
