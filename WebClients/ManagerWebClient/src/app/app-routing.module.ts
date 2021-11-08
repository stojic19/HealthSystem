import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FeedbacksManagerComponent } from './components/feedbacks-manager/feedbacks-manager.component';
import { FirstBuildingComponent } from './first-building/first-building.component';
import { HospitalOverviewComponent } from './hospital-overview/hospital-overview.component';
import { MaterialModule } from './material/material.module';

const routes: Routes = [
  { path: 'overview', component: HospitalOverviewComponent },
  { path: 'feedbacks', component: FeedbacksManagerComponent},
  { path: 'firstBuilding', component: FirstBuildingComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule, MaterialModule],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [HospitalOverviewComponent, FirstBuildingComponent, FeedbacksManagerComponent];
