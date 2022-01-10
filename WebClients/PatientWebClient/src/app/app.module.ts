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
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FeedbacksPageComponent } from './components/feedbacks-page/feedbacks-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainComponent } from './components/main/main.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { BasicAppointmentComponent } from './components/basic-appointment/basic-appointment.component';
import { AppointmentsPageComponent } from './components/appointments-page/appointments-page.component';
import { PatientMedicalRecordComponent } from './components/patient-medical-record/patient-medical-record.component';
import { SurveySectionComponent } from './components/survey-section/survey-section.component';
import { RecommendedAppointmentComponent } from './components/recommended-appointment/recommended-appointment.component';
import { LoginComponent } from './components/login/login.component';
import { SurveyPageComponent } from './components/survey-page/survey-page.component';
import { JwtInterceptor } from './JwtInterceptor/jwt-interceptor';
import { AuthGuard } from 'src/app/AuthGuard/AuthGuard';
import { AuthService } from './services/AuthService/auth.service';
import { LandingPageComponent } from './components/landing-page/landing-page.component';


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
    BasicAppointmentComponent,
    LoginComponent,
    PatientMedicalRecordComponent,
    AppointmentsPageComponent,
    SurveySectionComponent,
    LoginComponent,
    SurveyPageComponent,
    RecommendedAppointmentComponent,
    LandingPageComponent,
   
  ],
  imports: [
    BrowserModule,
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
  providers: [HttpClientModule,
  {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}],
  bootstrap: [AppComponent],
})
export class AppModule {}