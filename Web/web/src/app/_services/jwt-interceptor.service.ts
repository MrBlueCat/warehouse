import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JwtInterceptorService implements HttpInterceptor{

  constructor(private authenticationService: AuthenticationService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let token = this.authenticationService.currenUserToken;
      if (token) {
          request = request.clone({
              setHeaders: { 
                  Authorization: `Bearer ${token}`
              }
          });
      }
      return next.handle(request);
  }

}
