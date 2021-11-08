import { FeedbacksPageComponent } from './components/feedbacks-page/feedbacks-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { FeedbackComponent } from './components/feedback/feedback.component';
import { MaterialModule } from './material/material.module';

const routes: Routes = [
  { path: 'home', component: HomePageComponent },
  { path: 'feedbacks', component: FeedbacksPageComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }];

@NgModule({
  imports: [RouterModule.forRoot(routes), MaterialModule],
  exports: [RouterModule],
  entryComponents: [FeedbackComponent]
})
export class AppRoutingModule { }
