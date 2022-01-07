import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';
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
  imageSrc: SafeStyle  = "./assets/images/no-image.jpg";
  imageFile: File;

  constructor(private _route: ActivatedRoute, private _pharmacyService: PharmacyService, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    let id = Number(this._route.snapshot.paramMap.get('id'));
    this.id = id;

    this._pharmacyService.getPharmacyById(id)
    .subscribe(pharmacies => {this.pharmacy = pharmacies, this.getImage()},
      (error) => alert(error.error));
  }

  getImage(){
    this._pharmacyService.getImage(this.imageReq())
    .subscribe(image => {
      if(image != "err"){
        this.imageSrc = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + image);
      }},
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
    this._pharmacyService.updatePharmacy(this.makeRequest())
    .subscribe(res => {},
      (error) => alert(error.error)
      );

    const formData = new FormData();
    formData.append(this.imageFile.name, this.imageFile);
    this._pharmacyService.uploadImage(formData).subscribe(res => {},
      (error) => alert(error.error)
      );
  }

  makeRequest(){
    if(this.imageFile != null){
      let updatedPharmacy = {
        Id : this.id,
        PharmacyName: this.pharmacy.name,
        CityName: this.pharmacy.city.name,
        PostalCode: this.pharmacy.city.postalCode,
        StreetName: this.pharmacy.streetName,
        StreetNumber: this.pharmacy.streetNumber,
        CountryName: this.pharmacy.city.country.name,
        Description: this.pharmacy.description,
        ImageName: this.imageFile.name
      }
      return updatedPharmacy;
    }
    else{
      let updatedPharmacy = {
        Id : this.id,
        PharmacyName: this.pharmacy.name,
        CityName: this.pharmacy.city.name,
        PostalCode: this.pharmacy.city.postalCode,
        StreetName: this.pharmacy.streetName,
        StreetNumber: this.pharmacy.streetNumber,
        CountryName: this.pharmacy.city.country.name,
        Description: this.pharmacy.description,
        ImageName: this.pharmacy.imageName
      }
      return updatedPharmacy;
    }
  }

  imageReq(){
    var imageReq = {
      PharmacyId: this.pharmacy.id
    }
    return imageReq;
  }

}
