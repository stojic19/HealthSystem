import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ITenderingStatistics } from 'src/app/interfaces/tendering-statistics';
import { TenderingService } from 'src/app/services/tendering.service';

@Component({
  selector: 'app-tendering-statistics',
  templateUrl: './tendering-statistics.component.html',
  styleUrls: ['./tendering-statistics.component.css']
})
export class TenderingStatisticsComponent implements OnInit {

  constructor(private _TenderingService : TenderingService, private modalService: NgbModal) { }
  ngOnInit(): void {
  }
  stats : ITenderingStatistics
  tendersEnteredChartData = [{ name: "Placeholder", value: 10 }];
  tendersWonChartData = [{ name: "Placeholder", value: 10 }];
  tenderOffersMadeChartData = [{ name: "Placeholder", value: 10 }];
  profitMadeChartData = [{ name: "Placeholder", value: 10 }];

  GetStatistics(start: HTMLInputElement, end: HTMLInputElement, myModal)
  {
    if(start.value == "" || end.value == "")
    {
      alert("Please enter date range");
      return;
    }
    
    var startDate = new Date(start.value);
    var userTimezoneOffset = startDate.getTimezoneOffset() * 60000;
    startDate = new Date(startDate.getTime() - userTimezoneOffset);
    var endDate = new Date(end.value);
    var userTimezoneOffset = startDate.getTimezoneOffset() * 60000;
    endDate = new Date(endDate.getTime() - userTimezoneOffset);
    var timeRange = {startTime: startDate, endTime: endDate};
    this._TenderingService.getStatistics(timeRange).subscribe(data => {
      console.log(data);
      this.stats = data
      this.tendersEnteredChartData = []
      this.tendersWonChartData = []
      this.tenderOffersMadeChartData = []
      this.profitMadeChartData = []
      this.stats.pharmacyStatistics.forEach(pharmacyStatistic => {
        this.tendersEnteredChartData.push({ name: pharmacyStatistic.pharmacyName, value: pharmacyStatistic.tendersEntered })
        this.tendersWonChartData.push({ name: pharmacyStatistic.pharmacyName, value: pharmacyStatistic.tendersWon })
        this.tenderOffersMadeChartData.push({ name: pharmacyStatistic.pharmacyName, value: pharmacyStatistic.tenderOffersMade })
        this.profitMadeChartData.push({ name: pharmacyStatistic.pharmacyName, value: pharmacyStatistic.profit.amount })
      });
      this.modalService.open(myModal, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {});
    });
  }

}
