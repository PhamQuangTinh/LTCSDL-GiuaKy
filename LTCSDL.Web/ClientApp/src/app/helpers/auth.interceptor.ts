import { HTTP_INTERCEPTORS, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import {Observable} from 'rxjs'
import {tap} from 'rxjs/operators';
import { TokenStorageService } from '../services/token-storage.service';
import { Router } from '@angular/router';

const TOKEN_HEADER_KEY = 'Authorization';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private token: TokenStorageService, private router: Router) {

   }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authReq = req;
    
    const tokenRequest = this.token.getToken();

    //Gắn token vào header
    if (tokenRequest != null) {
      authReq = req.clone({ headers: req.headers.set(TOKEN_HEADER_KEY, 'Bearer ' + tokenRequest) });
      return next.handle(authReq).pipe(
        tap(
          succ =>{},
          err => {
            if(err.status == 401){
              localStorage.removeItem('auth-token');
              this.router.navigateByUrl('/trangchu/home');
            }
          },
          
        )
      );  
    }
    else {
      return next.handle(req.clone());
    }
    
  }
}

export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
];