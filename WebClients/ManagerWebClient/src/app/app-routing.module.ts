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
import { ComplaintsListComponent } from './complaints/complaints-list/complaints-list.component';
import { ComplaintDetailsComponent } from './complaints/complaint-details.component';
import { AddComplaintComponent } from './complaints/add-complaint/add-complaint.component';
import { PharmaciesListComponent } from './pharmacies/pharmacies-list.component';
import { RegisterPharmacyComponent } from './pharmacies/register-pharmacy/register-pharmacy.component';
import { HospitalEquipmentComponent } from './hospital-equipment/hospital-equipment.component';
import { BenefitListComponent } from './benefits/benefit-list/benefit-list.component';
import { BenefitDetailsComponent } from './benefits/benefit-details/benefit-details.component';
import { MedicationReportsComponent } from './medication-reports/medication-reports/medication-reports.component';
import { MedicineSpecificationRequestsComponent } from './medicine-specification-requests/medicine-specification-requests.component';
import { SurveysObserveComponent } from './components/surveys-observe/surveys-observe.component';
import { SurveySectionObserveComponent } from './components/survey-section-observe/survey-section-observe.component';
import { RatingDecimalComponent } from './components/rating-decimal/rating-decimal.component';
import { MedicineSpecificationListComponent } from './medicine-specification-requests/medicine-specification-list/medicine-specification-list.component';

const routes: Routes = [
  { path: 'overview', component: HospitalOverviewComponent },
  { path: 'feedbacks', component: FeedbacksManagerComponent },
  { path: 'firstBuilding', component: FirstBuildingComponent },
  { path: 'secondBuilding', component: SecondBuildingComponent },
  { path: 'roomInventory/:id', component: RoomInventoryComponent },
  { path: 'surveys', component: SurveysObserveComponent },
  { path: 'hospitalEquipment', component: HospitalEquipmentComponent },
  { path: 'firstBuilding/:roomName/:floor', component: FirstBuildingComponent },
  {
    path: 'secondBuilding/:roomName/:floor',
    component: SecondBuildingComponent,
  },
  { path: 'moveEquipment/:id', component: EquipmentFormComponent },
  { path: 'home', component: HomePageComponent },
  { path: 'medication-consumption-report', component: MedicationReportsComponent},
  { path: 'medicine-specification-requests', component: MedicineSpecificationListComponent},
  { path: 'new-medicine-specification-request', component: MedicineSpecificationRequestsComponent},
  { path: '', redirectTo: 'home', pathMatch: 'full'},
  { path: 'moveEquipment/:id', component: EquipmentFormComponent },
  { path: 'surveys', component: SurveysObserveComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule, MaterialModule],
  exports: [RouterModule],
})
export class AppRoutingModule { }
export const routingComponents = [HospitalOverviewComponent, FirstBuildingComponent, FeedbacksManagerComponent, SecondBuildingComponent, RoomInventoryComponent, HospitalEquipmentComponent,SurveysObserveComponent,SurveySectionObserveComponent,RatingDecimalComponent];
