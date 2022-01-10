import { Component } from '@angular/core';
import { ComplaintsService } from './services/complaints.service';
import { PharmacyService } from './services/pharmacy.service';
import { TenderService } from './services/tender.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ComplaintsService, PharmacyService, TenderService]
})
export class AppComponent {
  title = 'ManagerWebClient';
}
