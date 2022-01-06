import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/AuthService/auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor() { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Adding Auth header with JWT Token if User is logged and send it with Request
        request = request.clone({
                setHeaders: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                }
            });
        return next.handle(request);
    }
}