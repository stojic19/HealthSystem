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
  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<LogedManager>(JSON.parse((localStorage.getItem('currentUser'))!));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(model: any): Observable<LogedManager> {
    return this.http.post('api/Login', model).pipe(
      map((response: any) => {
        if (response && response.token) {
          localStorage.setItem('token', response.token);
          localStorage.setItem('currentUser', JSON.stringify(response));
          this.currentUserSubject.next(response);
          window.location.href = "http://localhost:4200/overview";
        }
        return this.user;

      })
    );
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return true;
  }

  logout() {
    this.user = {
      userName: null,
      email: null,
    };
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
  }

  public get currentUserValue(): LogedManager {
    return this.currentUserSubject.value;
  }
}

