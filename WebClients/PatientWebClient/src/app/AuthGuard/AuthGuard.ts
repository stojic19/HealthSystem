import { Injectable } from '@angular/core';
import { Router, CanActivate} from '@angular/router';
import { AuthService } from 'src/app/services/AuthService/auth.service';


@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthService
    ) { }

    canActivate() {
        const currentUser = this.authenticationService.currentUserValue;
        if (currentUser) { 
             return true;
        }

        this.router.navigate(['/login']); 
        return false;
    }
}