import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router:Router, private toastr :ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError( error =>{
          if(error){
            switch(error.status){
              case 400:
                if(error.error.errors){
                  const modalStateErrors = [];
                  for(const key in error.error.errors){
                    modalStateErrors.push(error.error.errors[key]);
                  }
                  console.log(modalStateErrors[0] + modalStateErrors[1]);
                  this.toastr.error(modalStateErrors[0] + modalStateErrors[1], "400")
                  throw modalStateErrors;
                } else{
                  this.toastr.error(error.error, error.status);
                } 
                break;
              case 401: 
                console.log(error);
                this.toastr.error(error.error, error.status);
                break;
              case 404:
                this.router.navigateByUrl('/not-found');
                this.toastr.error(error.error.title, error.status);
                break;
              case 500:
                const navigationExtras: NavigationExtras = {state: {error: error.error}};
                this.router.navigateByUrl('/server-error', navigationExtras);
                this.toastr.error(error.error.message, error.error.statusCode);
                break;
              default:
                break;
            }
          } 
          return throwError(error);
        }
      )
    );
  }
}
