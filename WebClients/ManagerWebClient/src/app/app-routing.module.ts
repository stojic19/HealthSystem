import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FirstBuildingComponent } from './first-building/first-building.component';
import { HospitalOverviewComponent } from './hospital-overview/hospital-overview.component';

const routes: Routes = [
  { path: 'overview', component: HospitalOverviewComponent },
  { path: 'firstBuilding', component: FirstBuildingComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [HospitalOverviewComponent, FirstBuildingComponent];
