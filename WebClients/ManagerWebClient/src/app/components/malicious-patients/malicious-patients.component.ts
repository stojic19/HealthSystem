import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { IMaliciousPatient, MaliciousPatient } from 'src/app/interfaces/malicious-patient';
import { MaliciousPatientsService } from 'src/app/services/malicious-patients.service';

@Component({
  selector: 'app-malicious-patients',
  templateUrl: './malicious-patients.component.html',
  styleUrls: ['./malicious-patients.component.css'],
  providers: [MaliciousPatientsService]
})
export class MaliciousPatientsComponent implements OnInit {

  maliciousPatients: IMaliciousPatient[];
  sub!: Subscription;
  constructor(private _maliciousPatientsService: MaliciousPatientsService, private _snackBar: MatSnackBar) {   }

  ngOnInit(): void {
    this.sub = this._maliciousPatientsService.getMaliciousPatients().subscribe({
      next: com => { this.maliciousPatients = com; }
    });
  }
  blockPatient(patient: IMaliciousPatient) {
    this._maliciousPatientsService.blockPatient(patient).subscribe({
      next: com => { this.maliciousPatients = com; }});
  }

}
