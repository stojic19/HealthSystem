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
import { RenovationFormComponent } from './renovation-form/renovation-form.component';
import { RoomScheduleComponent } from './room-schedule/room-schedule.component';
import { MaliciousPatientsComponent } from './components/malicious-patients/malicious-patients.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from 'src/app/AuthGuard/AuthGuard';


const routes: Routes = [
  { path: 'overview', component: HospitalOverviewComponent ,  canActivate: [AuthGuard]},
  { path: 'feedbacks', component: FeedbacksManagerComponent, canActivate: [AuthGuard] },
  { path: 'firstBuilding', component: FirstBuildingComponent ,  canActivate: [AuthGuard] },
  { path: 'secondBuilding', component: SecondBuildingComponent ,  canActivate: [AuthGuard]},
  { path: 'roomInventory/:id', component: RoomInventoryComponent ,  canActivate: [AuthGuard]},
  { path: 'hospitalEquipment', component: HospitalEquipmentComponent,  canActivate: [AuthGuard] },
  { path: 'firstBuilding/:roomName/:floor', component: FirstBuildingComponent , canActivate: [AuthGuard]},
  {
    path: 'secondBuilding/:roomName/:floor',
    component: SecondBuildingComponent,
  },
  { path: 'complaints', component: ComplaintsListComponent , canActivate: [AuthGuard]},
  { path: 'complaints/:id', component: ComplaintDetailsComponent , canActivate: [AuthGuard]},
  { path: 'complaint-add', component: AddComplaintComponent ,  canActivate: [AuthGuard]},
  { path: 'pharmacy-register', component: RegisterPharmacyComponent , canActivate: [AuthGuard]},
  { path: 'pharmacy-list', component: PharmaciesListComponent , canActivate: [AuthGuard]},
  { path: 'benefit-list', component: BenefitListComponent , canActivate: [AuthGuard] },
  { path: 'benefit/:id', component: BenefitDetailsComponent , canActivate: [AuthGuard]},
  { path: 'home', component: HomePageComponent , canActivate: [AuthGuard] },
  {
    path: 'medication-consumption-report',
    component: MedicationReportsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'medicine-specification-requests',
    component: MedicineSpecificationListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'new-medicine-specification-request',
    component: MedicineSpecificationRequestsComponent,
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'moveEquipment/:id', component: EquipmentFormComponent },
  { path: 'surveys', component: SurveysObserveComponent,canActivate: [AuthGuard]},
  { path: 'roomRenovation', component: RenovationFormComponent},
  { path: 'schedule/:id', component: RoomScheduleComponent },
  { path: 'blocking', component: MaliciousPatientsComponent,canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent},
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
