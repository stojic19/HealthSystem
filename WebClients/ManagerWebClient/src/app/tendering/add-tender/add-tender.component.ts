import { Component, OnInit } from '@angular/core';
import { TenderingService } from 'src/app/services/tendering.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-tender',
  templateUrl: './add-tender.component.html',
  styleUrls: ['./add-tender.component.css']
})

export class AddTenderComponent implements OnInit {
  
  medicines: any[] = [];
  SearchString: string = "";
  MedicineString: string = "";
  QuantityString: string = "";
  TenderNameString: string = "";
  nonFilteredMedicines: any = [];
  EditMedicineString: string = "";
  EditQuantityString: string = "";
  EditedMedicineName: string = "";

  constructor(private _tenderingService: TenderingService, private toastr: ToastrService) { }

  ngOnInit(): void {

  }

  FilterFn(){
    var searchString = this.SearchString.trim().toLowerCase();
    
    if(searchString == ""){
      this.medicines = this.nonFilteredMedicines;
      return;
    }

    this.medicines = [];
    for(var i = 0; i < this.nonFilteredMedicines.length; i++){
        if(this.nonFilteredMedicines[i].medicineName.toLowerCase().includes(searchString)){
          this.medicines.push(this.nonFilteredMedicines[i]);
        }
    }
  }

  Clear(){
    this.medicines = [];
    this.nonFilteredMedicines = [];
  }

  Edit(name: any, quantity: any){
    this.EditedMedicineName = name;
    this.EditMedicineString = name;
    this.EditQuantityString = quantity;
  }

  Save(){
    if(this.EditMedicineString === "" || this.EditMedicineString == null)
    {
      this.toastr.error("Enter valid medicine name!", "Error!");
      return;
    }
    if(this.EditQuantityString === "" || this.EditQuantityString == null || isNaN(Number(this.QuantityString)))
    {
      this.toastr.error("Enter valid medicine quantity!", "Error!");
      return;
    }
    var quantity = Number(this.EditQuantityString)
    if(quantity < 1)
    {
      this.toastr.error("Enter valid medicine quantity!", "Error!");
      return;
    }
    for(var i = 0; i < this.nonFilteredMedicines.length; i++){
      if(this.nonFilteredMedicines[i].medicineName === this.MedicineString && this.nonFilteredMedicines[i].medicineName != this.EditedMedicineName){     
        this.toastr.error("Enter unique medicine name!", "Error!");
        return;
      }
    }
    for(var i = 0; i < this.medicines.length; i++){
      if(this.medicines[i].medicineName === this.EditedMedicineName){     
        this.medicines[i].medicineName = this.EditMedicineString;
        this.medicines[i].quantity = this.EditQuantityString;
        break;
      }
    }
    for(var i = 0; i < this.nonFilteredMedicines.length; i++){
      if(this.nonFilteredMedicines[i].medicineName === this.EditedMedicineName){     
        this.nonFilteredMedicines[i].medicineName = this.EditMedicineString;
        this.nonFilteredMedicines[i].quantity = this.EditQuantityString;
        this.toastr.success("Medicine data updated successfully!", "Success!");
        return;
      }
    }
  }

  Add(){
    if(this.MedicineString === "" || this.MedicineString == null)
    {
      this.toastr.error("Enter valid medicine name!", "Error!");
      return;
    }
    if(this.QuantityString === "" || this.QuantityString == null || isNaN(Number(this.QuantityString)))
    {
      this.toastr.error("Enter valid medicine quantity!", "Error!");
      return;
    }
    var quantity = Number(this.QuantityString)
    if(quantity < 1)
    {
      this.toastr.error("Enter valid medicine quantity!", "Error!");
      return;
    }
    for(var i = 0; i < this.nonFilteredMedicines.length; i++){
      if(this.nonFilteredMedicines[i].medicineName === this.MedicineString){     
        this.toastr.error("Enter unique medicine name!", "Error!");
        return;
      }
    }
    var medicine = { medicineName: this.MedicineString, quantity: this.QuantityString}
    this.medicines.push(medicine);
    this.nonFilteredMedicines.push(medicine);
    this.MedicineString = "";
    this.QuantityString = "";
    this.toastr.success("Medicine succesfully added!", "Success!");
  }

  Delete(name: any){
    let nonFilteredMedicinesTemp = [] as  any
    for(var i = 0; i < this.nonFilteredMedicines.length; i++){
      if(this.nonFilteredMedicines[i].medicineName !== name){
        nonFilteredMedicinesTemp.push(this.nonFilteredMedicines[i]);
      }
    }
    let medicinesTemp = [] as  any
    for(var i = 0; i < this.medicines.length; i++){
      if(this.medicines[i].medicineName !== name){
        medicinesTemp.push(this.medicines[i]);
      }
    }
    this.medicines = medicinesTemp;
    this.nonFilteredMedicines = nonFilteredMedicinesTemp;
    this.toastr.success("Medicine succesfully deleted!", "Success!");
  }

  CreateTender(end: HTMLInputElement){
    if(this.TenderNameString === "" || this.TenderNameString == null)
    {
      this.toastr.error("Enter valid tender name!", "Error!");
      return;
    }
    if(this.nonFilteredMedicines.length < 1)
    {
      this.toastr.error("You must add at least one medicine!", "Error!");
      return;
    }
    var endDate;
    if(end.value !== "")
    {
      endDate = new Date(end.value);
    }
    var request = 
    {
      name : this.TenderNameString,
      endDate : endDate,
      medicineRequests : this.nonFilteredMedicines
    }
    this._tenderingService.createTender(request).subscribe(res =>
      {
        this.toastr.success(res.toString());
      },(error) => console.log(console.error()
      ));
  }
}
