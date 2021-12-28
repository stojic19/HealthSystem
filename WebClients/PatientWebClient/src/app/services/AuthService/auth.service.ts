import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable} from 'rxjs';
import { ILoginData } from 'src/app/interfaces/login-data';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<ILoginData>;
  public currentUser: Observable<ILoginData>;
  private user!: ILoginData;

  constructor(
    private _http: HttpClient, 
   ) {
      this.currentUserSubject = new BehaviorSubject<ILoginData>(JSON.parse((localStorage.getItem('currentUser'))!));
       this.currentUser = this.currentUserSubject.asObservable();
     }

     login(model: any): Observable<ILoginData> {
      return this._http.post('api/Login', model).pipe(
        map((response: any) => {
          if (response && response.token) {
            localStorage.setItem('token', response.token);
            localStorage.setItem('currentUser', JSON.stringify(response));
            this.currentUserSubject.next(response);
      
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
  
    public get currentUserValue(): ILoginData {
      return this.currentUserSubject.value;
    }

}
