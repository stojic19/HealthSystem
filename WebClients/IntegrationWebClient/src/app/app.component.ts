import { Component } from '@angular/core';
import { ComplaintsService } from './complaints/complaints.service';
import { PharmacyService } from './pharmacy/pharmacy.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ComplaintsService, PharmacyService]
})
export class AppComponent {
  title = 'manager-front';
}
