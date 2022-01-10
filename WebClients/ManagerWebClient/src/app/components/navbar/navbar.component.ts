import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  loginStatus$: Observable<boolean>;
  constructor(private authService: AuthService, private router: Router) {
    this.loginStatus$ = this.authService.isLoggedIn;
  }
  
    isProd = environment.production;

  ngOnInit(): void {
  }

  onLogout() {
    this.authService.logout();
    this.router.navigate(['']);
  }

}
