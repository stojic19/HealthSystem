import { Component, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationsService } from 'src/app/services/notifications.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  loginStatus$: Observable<boolean>;
  subscription : Subscription;
  notifications : Array<any> = [];
  constructor(private authService: AuthService, private router: Router, private _notificationsService: NotificationsService) {
    this.loginStatus$ = this.authService.isLoggedIn;
    this.subscription = router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this._notificationsService.getNotifications()
        .subscribe(notifications => {
          this.notifications = notifications
        });
      }
    });
  }
  
    isProd = environment.production;

  ngOnInit(): void {
  }

  onLogout() {
    this.authService.logout();
    this.router.navigate(['']);
  }

}
