import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { ILoginData } from 'src/app/interfaces/login-data';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<ILoginData>;
  public currentUser: Observable<ILoginData>;
  private user!: ILoginData;
  private loginStatus = new BehaviorSubject<boolean>(false);

  constructor(private _http: HttpClient, private _router: Router) {
    this.currentUserSubject = new BehaviorSubject<ILoginData>(
      JSON.parse(localStorage.getItem('currentUser')!)
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(model: any): Observable<ILoginData> {
    return this._http.post('api/Login', model).pipe(
      map((response: any) => {
        if (response && response.token) {
          console.log('ovov' + response.token);
          this.loginStatus.next(true);
          localStorage.setItem('token', response.token);
          localStorage.setItem('currentUser', JSON.stringify(response));
          this.currentUserSubject.next(response);
          this._router.navigate(['/record']);
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
    this._router.navigate(['/login']);
  }

  public get currentUserValue(): ILoginData {
    return this.currentUserSubject.value;
  }
}
