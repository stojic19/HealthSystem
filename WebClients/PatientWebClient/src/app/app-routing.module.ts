import { FeedbacksPageComponent } from './components/feedbacks-page/feedbacks-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { FeedbackComponent } from './components/feedback/feedback.component';
import { MaterialModule } from './material/material.module';
import { MainComponent } from './components/main/main.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { PatientMedicalRecordComponent } from './components/patient-medical-record/patient-medical-record.component';
import { AppointmentsPageComponent } from './components/appointments-page/appointments-page.component';
import { SurveyPageComponent } from './components/survey-page/survey-page.component';
import { RecommendedAppointmentComponent } from './components/recommended-appointment/recommended-appointment.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthGuard } from './AuthGuard/AuthGuard';
import { LandingPageComponent } from './components/landing-page/landing-page.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: '',
        component: LandingPageComponent,
      },
      {
        path: 'feedbacks',
        component: FeedbacksPageComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'record',
        component: PatientMedicalRecordComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'survey/:appointmentId',
        component: SurveyPageComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'login',
        component: LoginComponent,
      },
    ],
  },
  { path: 'registration', component: RegistrationComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'recommendedAppointments',
    component: RecommendedAppointmentComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    HttpClientModule,
  ],
  exports: [RouterModule, MaterialModule, FormsModule, ReactiveFormsModule],
  entryComponents: [
    FeedbackComponent,
    AppointmentsPageComponent,
    SurveyPageComponent,
  ],
})
export class AppRoutingModule {}
