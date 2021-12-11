import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { HomePageComponent } from './components/home-page/home-page.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { FeedbackComponent } from './components/feedback/feedback.component';
import { PatientFeedbackComponent } from './components/patient-feedback/patient-feedback.component';
import { HttpClientModule } from '@angular/common/http';
import { FeedbacksPageComponent } from './components/feedbacks-page/feedbacks-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainComponent } from './components/main/main.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { AppointmentsPageComponent } from './components/appointments-page/appointments-page.component';
import { SurveyPageComponent } from './components/survey-page/survey-page.component';
import { SurveySectionComponent } from './components/survey-section/survey-section.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    HeaderComponent,
    FooterComponent,
    FeedbackComponent,
    PatientFeedbackComponent,
    FeedbacksPageComponent,
    MainComponent,
    RegistrationComponent,
    AppointmentsPageComponent,
    SurveySectionComponent,
  ],
  imports: [
    AppRoutingModule,
    NgbModule,
    BrowserAnimationsModule,
    MaterialModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [HttpClientModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
