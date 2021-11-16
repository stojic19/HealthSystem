import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedbacksManagerComponent } from './components/feedbacks-manager/feedbacks-manager.component';
import { FirstBuildingComponent } from './first-building/first-building.component';
import { HospitalOverviewComponent } from './hospital-overview/hospital-overview.component';
import { MaterialModule } from './material/material.module';
import { SecondBuildingComponent } from './second-building/second-building.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { EquipmentFormComponent } from './equipment-form/equipment-form.component';
import { RoomInventoryComponent } from './room-inventory/room-inventory.component';
import { RoomInfoComponent } from './room-info/room-info.component';
import { HospitalEquipmentComponent } from './hospital-equipment/hospital-equipment.component';

const routes: Routes = [
  { path: 'overview', component: HospitalOverviewComponent },
  { path: 'feedbacks', component: FeedbacksManagerComponent },
  { path: 'firstBuilding', component: FirstBuildingComponent },
  { path: 'secondBuilding', component: SecondBuildingComponent },
  { path: 'roomInventory/:id', component: RoomInventoryComponent },
  { path: 'hospitalEquipment', component: HospitalEquipmentComponent },
  { path: 'home', component: HomePageComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule, MaterialModule],
  exports: [RouterModule],
})
export class AppRoutingModule { }
export const routingComponents = [HospitalOverviewComponent, FirstBuildingComponent, FeedbacksManagerComponent, SecondBuildingComponent, RoomInventoryComponent, HospitalEquipmentComponent];
