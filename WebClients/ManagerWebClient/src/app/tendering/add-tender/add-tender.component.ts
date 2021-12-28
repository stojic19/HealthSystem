import { Component, OnInit } from '@angular/core';

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
  EditMedicineString: string = "";
  EditQuantityString: string = "";
  TenderNameString: string = "";
  nonFilteredMedicines: any = [];
  ModalHidden: boolean = true;
  ModalButton: HTMLElement = document.getElementById("openModalButton") as HTMLElement;
  constructor() { }

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
        if(this.nonFilteredMedicines[i].name.toLowerCase().includes(searchString)){
          this.medicines.push(this.nonFilteredMedicines[i]);
        }
    }
  }

  Clear(){
    this.medicines = [];
    this.nonFilteredMedicines = [];
  }

  Edit(name: any){
    let element: HTMLElement = document.getElementById("openModalButton") as HTMLElement;
    console.log(name);
    for(var i = 0; i < this.nonFilteredMedicines.length; i++){
      console.log(this.nonFilteredMedicines[i].name);
      if(this.nonFilteredMedicines[i].name === name){
          this.EditMedicineString = this.MedicineString;
          this.EditQuantityString = this.EditQuantityString;
          this.ModalButton.click();
      }
    }
  }

  Add(){
    for(var i = 0; i < this.nonFilteredMedicines.length; i++){
      if(this.nonFilteredMedicines[i].name === this.MedicineString){     
        return;
      }
    }
    var medicine = { name: this.MedicineString, quantity: this.QuantityString}
    this.medicines.push(medicine);
    this.nonFilteredMedicines.push(medicine);
    this.MedicineString = "";
    this.QuantityString = "";
  }

  Delete(name: any){
    console.log(name);
    let nonFilteredMedicinesTemp = [] as  any
    for(var i = 0; i < this.nonFilteredMedicines.length; i++){
      console.log(this.nonFilteredMedicines[i].name)
      if(this.nonFilteredMedicines[i].name !== name){
        nonFilteredMedicinesTemp.push(this.nonFilteredMedicines[i]);
      }
    }
    let medicinesTemp = [] as  any
    for(var i = 0; i < this.medicines.length; i++){
      if(this.medicines[i].name !== name){
        medicinesTemp.push(this.medicines[i]);
      }
    }
    this.medicines = medicinesTemp;
    this.nonFilteredMedicines = nonFilteredMedicinesTemp;
  }

  CreateTender(){

  }
}
