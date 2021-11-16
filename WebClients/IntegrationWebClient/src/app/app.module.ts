import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComplaintsListComponent } from './complaints/complaints-list.component';
import { ComplaintDetailsComponent } from './complaints/complaint-details.component';
import { AddComplaintComponent } from './complaints/add-complaint.component';
import { RegisterPharamcyComponent } from './pharmacy/register-pharamcy.component';
import { PharmacyListComponent } from './pharmacy-list/pharmacy-list.component';

@NgModule({
  declarations: [
    AppComponent,
    ComplaintsListComponent,
    ComplaintDetailsComponent,
    AddComplaintComponent,
    RegisterPharamcyComponent,
    PharmacyListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
