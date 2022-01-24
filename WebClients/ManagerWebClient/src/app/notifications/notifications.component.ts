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
  notifications: any[] = [];

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
