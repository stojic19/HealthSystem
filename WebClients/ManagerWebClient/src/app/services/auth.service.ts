import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { ILogedManager, LogedManager } from '../interfaces/loged-manager';
import { Router } from '@angular/router';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl: string = environment.baseHospitalUrl;
  private currentUserSubject: BehaviorSubject<LogedManager>;
  public currentUser: Observable<LogedManager>;
  private user: LogedManager;
  private router: Router;
  private loginStatus = new BehaviorSubject<boolean>(false);
  constructor(private http: HttpClient,private _router: Router) {
    this.currentUserSubject = new BehaviorSubject<LogedManager>(JSON.parse((localStorage.getItem('currentUser'))!));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(model: any): Observable<LogedManager> {
    return this.http.post('api/Login', model).pipe(
      map((response: any) => {
        if (response && response.token) {
          this.loginStatus.next(true);
          localStorage.setItem('token', response.token);
          localStorage.setItem('currentUser', JSON.stringify(response));
          this.currentUserSubject.next(response);
          this._router.navigate(['/overview']);;
        }
        return this.user;

      })
    );
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return true;
  }

  get isLoggedIn() {
    return this.loginStatus.asObservable();
  }


  logout() {
      this.user = {
        userName: null,
        email: null,
      };
      this.loginStatus.next(false);
      localStorage.removeItem('token');
      localStorage.removeItem('currentUser');
      localStorage.setItem('loginStatus', '0');
    }
  

  public get currentUserValue(): LogedManager {
    return this.currentUserSubject.value;
  }
}

