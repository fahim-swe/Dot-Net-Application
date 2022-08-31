import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { delay, finalize, Observable } from 'rxjs';
import { BusyService } from '../_services/busy.service';
import { environment } from '../../environments/environment';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    console.log(request.url);
    if(request.url.match(environment.apiUrl + "Messages")) {
      return next.handle(request);
    }
    this.busyService.busy();
        
    return next.handle(request).pipe(
      finalize(()=>{
        this.busyService.idle();
      })
    )
  }
}
