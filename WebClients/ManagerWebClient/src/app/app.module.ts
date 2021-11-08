import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HospitalOverviewComponent } from './hospital-overview/hospital-overview.component';
import { LegendComponent } from './hospital-overview/legend/legend.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FirstBuildingComponent } from './first-building/first-building.component';
import { FloorSelectionComponent } from './first-building/floor-selection/floor-selection.component';
import { FirstFloorComponent } from './first-building/first-floor/first-floor.component';
import { SecondFloorComponent } from './first-building/second-floor/second-floor.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FeedbacksManagerComponent } from './components/feedbacks-manager/feedbacks-manager.component';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [AppComponent, HospitalOverviewComponent, LegendComponent, NavbarComponent, FirstBuildingComponent, FloorSelectionComponent, FirstFloorComponent, SecondFloorComponent, FeedbacksManagerComponent],
  imports: [BrowserModule, AppRoutingModule,  NgbModule, CommonModule, HttpClientModule, BrowserAnimationsModule],
  providers: [HttpClientModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
