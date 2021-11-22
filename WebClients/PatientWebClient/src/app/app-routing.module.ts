import { SurveyListComponent } from './components/survey-list/survey-list.component';
import { SurveyComponent } from './components/survey/survey.component';
import { SurveyPageComponent } from './components/survey-page/survey-page.component';
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
        path: 'surveys',
        component: SurveyListComponent,
      },
      {
        path: 'surveys/:id',
        component: SurveyPageComponent,
      },
    ],
  },
  { path: 'registration', component: RegistrationComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
  ],
  exports: [RouterModule, MaterialModule, FormsModule],
  entryComponents: [FeedbackComponent, SurveyComponent],
})
export class AppRoutingModule { }
