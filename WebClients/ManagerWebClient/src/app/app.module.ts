import { NgModule } from '@angular/core';
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
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [AppComponent, HospitalOverviewComponent, LegendComponent, NavbarComponent, FirstBuildingComponent, FloorSelectionComponent, FirstFloorComponent, SecondFloorComponent, FeedbacksManagerComponent, SecondBuildingComponent, FloorFirstComponent, FloorSecondComponent, SelectFloorComponent, HomePageComponent, FooterComponent, RoomInfoComponent, DisplayRoomInfoComponent, EditRoomInfoComponent],
  imports: [BrowserModule, AppRoutingModule,  NgbModule, CommonModule, HttpClientModule, FormsModule , BrowserAnimationsModule],
  providers: [HttpClientModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
