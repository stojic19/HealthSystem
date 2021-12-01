import { NgModule } from '@angular/core';
import { MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HospitalOverviewComponent } from './hospital-overview/hospital-overview.component';
import { LegendComponent } from './hospital-overview/legend/legend.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FirstBuildingComponent } from './first-building/first-building.component';
import { FloorSelectionComponent } from './first-building/floor-selection/floor-selection.component';
import { FirstFloorComponent } from './first-building/first-floor/first-floor.component';
import { SecondFloorComponent } from './first-building/second-floor/second-floor.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FeedbacksManagerComponent } from './components/feedbacks-manager/feedbacks-manager.component';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SecondBuildingComponent } from './second-building/second-building.component';
import { FloorFirstComponent } from './second-building/floor-first/floor-first.component';
import { FloorSecondComponent } from './second-building/floor-second/floor-second.component';
import { SelectFloorComponent } from './second-building/select-floor/select-floor.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { FooterComponent } from './components/footer/footer.component';
import { RoomInfoComponent } from './room-info/room-info.component';
import { DisplayRoomInfoComponent } from './room-info/display-room-info/display-room-info.component';
import { EditRoomInfoComponent } from './room-info/edit-room-info/edit-room-info.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EquipmentFormComponent } from './equipment-form/equipment-form.component';
import { InitialRoomComponent } from './equipment-form/initial-room/initial-room.component';
import { DestinationRoomComponent } from './equipment-form/destination-room/destination-room.component';
import { MoveInfoComponent } from './equipment-form/move-info/move-info.component';
import { FreeTermsComponent } from './equipment-form/free-terms/free-terms.component';
import { SearchBarComponent } from './first-building/search-bar/search-bar.component';

import { QuestionObserveComponent } from './components/question-observe/question-observe.component';
import { RatingDecimalComponent } from './components/rating-decimal/rating-decimal.component';

import { RoomInventoryComponent } from './room-inventory/room-inventory.component';
import { ComplaintDetailsComponent } from './complaints/complaint-details.component';
import { AddComplaintComponent } from './complaints/add-complaint/add-complaint.component';
import { ComplaintsListComponent } from './complaints/complaints-list/complaints-list.component';
import { RegisterPharmacyComponent } from './pharmacies/register-pharmacy/register-pharmacy.component';
import { PharmaciesListComponent } from './pharmacies/pharmacies-list.component';
import { HospitalEquipmentComponent } from './hospital-equipment/hospital-equipment.component';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MedicineSpecificationRequestsComponent } from './medicine-specification-requests/medicine-specification-requests.component';
import { SurveySectionObserveComponent } from './components/survey-section-observe/survey-section-observe.component';
import { SurveysObserveComponent } from './components/surveys-observe/surveys-observe.component';
import { MaterialModule } from './material/material.module';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { MedicationReportsComponent } from './medication-reports/medication-reports/medication-reports.component';
import { BenefitListComponent } from './benefits/benefit-list/benefit-list.component';
import { BenefitDetailsComponent } from './benefits/benefit-details/benefit-details.component';

@NgModule({
  declarations: [
    AppComponent,
    HospitalOverviewComponent,
    LegendComponent,
    NavbarComponent,
    FirstBuildingComponent,
    FloorSelectionComponent,
    FirstFloorComponent,
    SecondFloorComponent,
    FeedbacksManagerComponent,
    SecondBuildingComponent,
    FloorFirstComponent,
    FloorSecondComponent,
    SelectFloorComponent,
    HomePageComponent,
    FooterComponent,
    RoomInfoComponent,
    DisplayRoomInfoComponent,
    EditRoomInfoComponent,
    EquipmentFormComponent,
    InitialRoomComponent,
    DestinationRoomComponent,
    MoveInfoComponent,
    FreeTermsComponent,
    SearchBarComponent,
    RoomInventoryComponent,
    HospitalEquipmentComponent,
    ComplaintDetailsComponent,
    AddComplaintComponent,
    ComplaintsListComponent,
    RegisterPharmacyComponent,
    PharmaciesListComponent,
    MedicationReportsComponent,
    BenefitListComponent,
    BenefitDetailsComponent,
    MedicineSpecificationRequestsComponent,
    RatingDecimalComponent,
    SurveySectionObserveComponent,
    SurveysObserveComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MaterialModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatOptionModule,
    MatSelectModule,
    MaterialModule,
  ],
  providers: [HttpClientModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
