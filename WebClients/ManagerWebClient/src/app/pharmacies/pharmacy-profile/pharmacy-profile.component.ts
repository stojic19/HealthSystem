import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ITenderStats } from 'src/app/interfaces/tender-statistic';
import { PharmacyService } from 'src/app/services/pharmacy.service';
import { TenderService } from 'src/app/services/tender.service';

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
  tenderStats: ITenderStats;
  chartData = [
    { name: "Tender offers", value: 10 },
    { name: "Won", value: 3 }
  ];

  constructor(private _route: ActivatedRoute, private _pharmacyService: PharmacyService, private _tenderService: TenderService, private sanitizer: DomSanitizer, private modalService: NgbModal) { }

  ngOnInit(): void {
    let id = Number(this._route.snapshot.paramMap.get('id'));
    this.id = id;

    this._pharmacyService.getPharmacyById(id)
    .subscribe(pharmacies => {this.pharmacy = pharmacies, this.getImage()},
      (error) => alert(error.error));

    this._tenderService.getTenderStatsForPharmacy(id)
    .subscribe(stats => {this.tenderStats = stats,
      this.chartData = [
        { name: "Tender offers", value: this.tenderStats.offers },
        { name: "Won", value: this.tenderStats.won }
      ]},
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

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {});
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
