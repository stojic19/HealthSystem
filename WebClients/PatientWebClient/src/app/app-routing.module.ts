import { FeedbacksPageComponent } from './components/feedbacks-page/feedbacks-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { FeedbackComponent } from './components/feedback/feedback.component';
import { MaterialModule } from './material/material.module';
import { MainComponent } from './components/main/main.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { AppointmentsPageComponent } from './components/appointments-page/appointments-page.component';
import { SurveyPageComponent } from './components/survey-page/survey-page.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: '',
        component: HomePageComponent,
      },
      {
        path: 'feedbacks',
        component: FeedbacksPageComponent,
      },
      {
        path:'appointments',
        component: AppointmentsPageComponent
      },
      {
        path:'surveys',
        component: SurveyPageComponent
      }
    ],
  },
  { path: 'registration', component: RegistrationComponent },
  { path: 'login', component: LoginComponent },
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
  exports: [RouterModule, MaterialModule, FormsModule],
  entryComponents: [FeedbackComponent, AppointmentsPageComponent, SurveyPageComponent],
})
export class AppRoutingModule {}
