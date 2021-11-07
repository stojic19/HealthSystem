import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddComplaintComponent } from './complaints/add-complaint.component';
import { ComplaintDetailsComponent } from './complaints/complaint-details.component';
import { ComplaintsListComponent } from './complaints/complaints-list.component';
import { RegisterPharamcyComponent } from './pharmacy/register-pharamcy.component';

const routes: Routes = [
  {path: 'complaints', component: ComplaintsListComponent},
  {path: 'complaints/:id', component: ComplaintDetailsComponent},
  {path: 'complaint-add', component: AddComplaintComponent},
  {path: 'pharmacy-register', component: RegisterPharamcyComponent },
  {path: '', redirectTo: 'complaints', pathMatch: 'full'},
  {path: '**', redirectTo: 'complaints', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
