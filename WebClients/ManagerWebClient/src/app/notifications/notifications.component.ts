import { Component, OnInit } from '@angular/core';
import { NotificationsService } from '../services/notifications.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {
  notifications: any[] = [{id: 1, title: "Naslov notifikacije", description: "opis", date: "19.1.2022."},
  {id: 2, title: "Naslov notifikacije 2", description: "opis2", date: "19.1.2022."}];

  constructor(private _notificationsService: NotificationsService, private toastr: ToastrService, private router:Router) { }

  ngOnInit(): void {
    this._notificationsService.getNotifications().subscribe(notifications => this.notifications = notifications);
  }

  delete(id){
    this._notificationsService.seen(id).subscribe(res =>
      {
        this.ngOnInit();
      },(error) => this.toastr.error(error.error, "Error!"));
  }

}
