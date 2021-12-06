import { Component, OnInit } from '@angular/core';
import { MedicineSpecificationRequestsService } from 'src/app/services/medicine-specification-requests.service';

@Component({
  selector: 'app-medicine-specification-list',
  templateUrl: './medicine-specification-list.component.html',
  styleUrls: ['./medicine-specification-list.component.css']
})
export class MedicineSpecificationListComponent implements OnInit {

  specifications: any[] = []
  answer: any

  constructor(private _medicineSpecificationService: MedicineSpecificationRequestsService) { }

  ngOnInit(): void {
    this._medicineSpecificationService.getAllMedicineSpecificationFiles()
            .subscribe(specifications => this.specifications = specifications);
  }

  showPDF(fileName: any): void {
    this._medicineSpecificationService.openPdf(fileName)
  }

}