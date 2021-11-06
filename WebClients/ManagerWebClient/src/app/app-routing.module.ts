import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HospitalOverviewComponent } from './hospital-overview/hospital-overview.component';

const routes: Routes = [
  { path: 'overview', component: HospitalOverviewComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [HospitalOverviewComponent];
