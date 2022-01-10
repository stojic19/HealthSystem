import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/AuthService/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  loginStatus$: Observable<boolean>;
  constructor(private authService: AuthService, private router: Router) {
    this.loginStatus$ = this.authService.isLoggedIn;
  }

  ngOnInit(): void {}

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
