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
import { PharmacyProfileComponent } from './pharmacies/pharmacy-profile/pharmacy-profile.component';
import { RenovationFormComponent } from './renovation-form/renovation-form.component';
import { RoomScheduleComponent } from './room-schedule/room-schedule.component';
import { MaliciousPatientsComponent } from './components/malicious-patients/malicious-patients.component';

import { environment } from 'src/environments/environment';

const _isProd = environment.production;

const routes: Routes = [
  { path: _isProd? 'manager/overview' : 'overview', component: HospitalOverviewComponent },
  { path: _isProd? 'manager/feedbacks' : 'feedbacks', component: FeedbacksManagerComponent },
  { path: _isProd? 'manager/firstBuilding' : 'firstBuilding', component: FirstBuildingComponent },
  { path: _isProd? 'manager/secondBuilding' : 'secondBuilding', component: SecondBuildingComponent },
  { path: _isProd? 'manager/roomInventory/:id' : 'roomInventory/:id', component: RoomInventoryComponent },
  { path: _isProd? 'manager/hospitalEquipment' : 'hospitalEquipment', component: HospitalEquipmentComponent },
  { path: _isProd? 'manager/firstBuilding/:roomName/:floor' : 'firstBuilding/:roomName/:floor', component: FirstBuildingComponent },
  {
    path: _isProd? 'manager/secondBuilding/:roomName/:floor' : 'secondBuilding/:roomName/:floor',
    component: SecondBuildingComponent,
  },
  { path: 'complaints', component: ComplaintsListComponent },
  { path: 'complaints/:id', component: ComplaintDetailsComponent },
  { path: 'complaint-add', component: AddComplaintComponent },
  { path: 'pharmacy-register', component: RegisterPharmacyComponent },
  { path: 'pharmacy-list', component: PharmaciesListComponent },
  { path: 'pharmacy-profile/:id', component: PharmacyProfileComponent },
  { path: 'benefit-list', component: BenefitListComponent },
  { path: 'benefit/:id', component: BenefitDetailsComponent },
  { path: 'home', component: HomePageComponent },
 
  {
    path: _isProd? 'manager/medication-consumption-report' : 'medication-consumption-report',
    component: MedicationReportsComponent,
  },
  {
    path: _isProd? 'manager/medicine-specification-requests' : 'medicine-specification-requests',
    component: MedicineSpecificationListComponent,
  },
  {
    path: _isProd? 'manager/new-medicine-specification-request' : 'new-medicine-specification-request',
    component: MedicineSpecificationRequestsComponent,
  },
  { path: _isProd? 'manager/roomRenovation' : 'roomRenovation', component: RenovationFormComponent},
  { path: _isProd? 'manager/blocking' : 'blocking', component: MaliciousPatientsComponent},
  { path: _isProd? 'manager' : '', redirectTo: _isProd? 'manager/home' : 'home', pathMatch: 'full' },
  { path: _isProd? 'manager/moveEquipment/:id' : 'moveEquipment/:id', component: EquipmentFormComponent },
  { path: _isProd? 'manager/surveys' : 'surveys', component: SurveysObserveComponent },
  { path: _isProd? 'manager/schedule/:id' : 'schedule/:id', component: RoomScheduleComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule, MaterialModule],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [
  HospitalOverviewComponent,
  FirstBuildingComponent,
  FeedbacksManagerComponent,
  SecondBuildingComponent,
  RoomInventoryComponent,
  HospitalEquipmentComponent,
  SurveysObserveComponent,
  SurveySectionObserveComponent,
  RatingDecimalComponent,
  MaliciousPatientsComponent
];
