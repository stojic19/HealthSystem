import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HopsitalOverviewComponent } from './hopsital-overview/hopsital-overview.component';

const routes: Routes = [
  { path: 'overview', component: HopsitalOverviewComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [HopsitalOverviewComponent];
