import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddComplaintComponent } from './complaints/add-complaint.component';
import { ComplaintDetailsComponent } from './complaints/complaint-details.component';
import { ComplaintsListComponent } from './complaints/complaints-list.component';
import { RegisterPharamcyComponent } from './pharmacy/register-pharamcy.component';
import { PharmacyListComponent } from './pharmacy-list/pharmacy-list.component';
import { environment } from 'src/environments/environment';

let isProd = environment.production;

const routes: Routes = [
  {path: isProd? 'integration/complaints' : 'complaints', component: ComplaintsListComponent},
  {path: isProd? 'integration/complaints/:id' : 'complaints/:id', component: ComplaintDetailsComponent},
  {path: isProd? 'integration/complaint-add' : 'complaint-add', component: AddComplaintComponent},
  {path: isProd? 'integration/pharmacy-register' : 'pharmacy-register', component: RegisterPharamcyComponent },
  {path: isProd? 'integration/pharmacy-list' : 'pharmacy-list', component: PharmacyListComponent },
  {path: isProd? 'integration' : '', redirectTo: isProd? 'integration/complaints' : 'complaints', pathMatch: 'full'},
  {path: isProd? 'integration/**' : '**', redirectTo: isProd? 'integration/complaints' : 'complaints', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
