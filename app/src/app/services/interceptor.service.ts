import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { TokenStorageService } from './token-storage.service';
import { catchError } from 'rxjs/operators'; 

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  constructor(private tokenStorage: TokenStorageService,private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
    const token : string = this.tokenStorage.getToken();
    let request = req;

    if(token) {
      request = req.clone({
        setHeaders:{
          autorization : `Bearer ${token}`
        }
      });
    }

    return next.handle(request).pipe(
      catchError((err:HttpErrorResponse) => {
        if(err.status === 401){
          this.router.navigateByUrl('/login');
        }
        return throwError(err);
      })
    );
  }
}
