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
import { AddTenderComponent } from './tendering/add-tender/add-tender.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from 'src/app/AuthGuard/AuthGuard';

import { environment } from 'src/environments/environment';
import { HospitalShiftsComponent } from './hospital-shifts/hospital-shifts.component';
import { CreateShiftComponent } from './create-shift/create-shift.component';
import { UpdateShiftComponent } from './update-shift/update-shift.component';
import { DoctorShiftComponent } from './doctor-shift/doctor-shift.component';

const _isProd = environment.production;

const routes: Routes = [
  { path: _isProd? 'manager/overview' : 'overview', component: HospitalOverviewComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/feedbacks' : 'feedbacks', component: FeedbacksManagerComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/firstBuilding' : 'firstBuilding', component: FirstBuildingComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/secondBuilding' : 'secondBuilding', component: SecondBuildingComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/roomInventory/:id' : 'roomInventory/:id', component: RoomInventoryComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/hospitalEquipment' : 'hospitalEquipment', component: HospitalEquipmentComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/firstBuilding/:roomName/:floor' : 'firstBuilding/:roomName/:floor', component: FirstBuildingComponent , canActivate: [AuthGuard]},
  {
    path: _isProd? 'manager/secondBuilding/:roomName/:floor' : 'secondBuilding/:roomName/:floor',
    component: SecondBuildingComponent , canActivate: [AuthGuard]
  },
  { path: _isProd? 'manager/complaints' : 'complaints', component: ComplaintsListComponent, canActivate: [AuthGuard]},
  { path: _isProd? 'manager/complaints/:id' : 'complaints/:id', component: ComplaintDetailsComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/complaint-add' : 'complaint-add', component: AddComplaintComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/pharmacy-register' : 'pharmacy-register', component: RegisterPharmacyComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/pharmacy-list' : 'pharmacy-list', component: PharmaciesListComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/benefit-list' : 'benefit-list', component: BenefitListComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/benefit/:id' : 'benefit/:id', component: BenefitDetailsComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/home' : 'home', component: HomePageComponent },
  {
    path: _isProd? 'manager/medication-consumption-report' : 'medication-consumption-report',
    component: MedicationReportsComponent, canActivate: [AuthGuard]
  },
  {
    path: _isProd? 'manager/medicine-specification-requests' : 'medicine-specification-requests',
    component: MedicineSpecificationListComponent, canActivate: [AuthGuard]
  },
  {
    path: _isProd? 'manager/new-medicine-specification-request' : 'new-medicine-specification-request',
    component: MedicineSpecificationRequestsComponent, canActivate: [AuthGuard]
  },
  { path: _isProd? 'manager/roomRenovation' : 'roomRenovation', component: RenovationFormComponent, canActivate: [AuthGuard]},
  { path: _isProd? 'manager/blocking' : 'blocking', component: MaliciousPatientsComponent, canActivate: [AuthGuard]},
  { path: _isProd? 'manager' : '', redirectTo: _isProd? 'manager/home' : 'home', pathMatch: 'full' },
  { path: _isProd? 'manager/add-tender' : 'add-tender', component: AddTenderComponent },
  { path: _isProd? 'manager/moveEquipment/:id' : 'moveEquipment/:id', component: EquipmentFormComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/surveys' : 'surveys', component: SurveysObserveComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/schedule/:id' : 'schedule/:id', component: RoomScheduleComponent , canActivate: [AuthGuard]},
  { path: _isProd? 'manager/login' : 'login', component: LoginComponent},
  { path: _isProd? 'manager/hospitalShifts' : 'hospitalShifts', component: HospitalShiftsComponent, canActivate: [AuthGuard]},
  { path: _isProd? 'manager/createNewShift' : 'createNewShift', component: CreateShiftComponent, canActivate: [AuthGuard]},
  { path: _isProd? 'manager/updateShift/:id' : 'updateShift/:id', component: UpdateShiftComponent, canActivate: [AuthGuard]},
  { path: _isProd? 'manager/doctorShifts' : 'doctorShifts', component: DoctorShiftComponent, canActivate: [AuthGuard]},

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
  MaliciousPatientsComponent,
  HospitalShiftsComponent,
  DoctorShiftComponent
];
