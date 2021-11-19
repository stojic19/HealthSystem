import { Component } from '@angular/core';
import { ComplaintsService } from './services/complaints.service';
import { PharmacyService } from './services/pharmacy.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ComplaintsService, PharmacyService]
})
export class AppComponent {
  title = 'ManagerWebClient';
}
