import { Component } from '@angular/core';
import { ComplaintsService } from './services/complaints.service';
import { NotificationsService } from './services/notifications.service';
import { PharmacyService } from './services/pharmacy.service';
import { TenderService } from './services/tender.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ComplaintsService, PharmacyService, TenderService, NotificationsService]
})
export class AppComponent {
  title = 'ManagerWebClient';
}
