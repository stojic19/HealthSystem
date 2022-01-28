import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { IReport } from 'src/app/interfaces/report';

import { MedicalRecordService } from 'src/app/services/MedicalRecordService/medicalrecord.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {
    sub!: Subscription;
    report!: IReport;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: number,
    public dialogRef: MatDialogRef<ReportComponent>,
    private service: MedicalRecordService
  ) {
    this.sub = this.service.getReportForEvent(this.data)
    .subscribe({
      next:(res: IReport)=>{
        this.report = res;
        console.log(res);
      }
    });
   }

  ngOnInit(): void {
  }
  
  

}
