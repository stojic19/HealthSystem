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

  constructor(private _route: ActivatedRoute, private _pharmacyService: PharmacyService) { }

  ngOnInit(): void {
    let id = Number(this._route.snapshot.paramMap.get('id'));
    this.id = id;

    //add http get method
  }

  onFileChange(event) {
    const reader = new FileReader();
    
    if(event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {this.imageSrc = reader.result as string;};

    }
  }

  UpdatePharmacy(){
    //add http post method
  }

}
