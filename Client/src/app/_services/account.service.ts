import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { User } from '../_models/user';
import { environment } from '../../environments/environment.prod';
import { PresenceService } from './presence.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient, private presence : PresenceService) { }

  login(model:any)
  {
    return this.http.post(this.baseUrl + "Accout/login", model).pipe(
      map( (response: User)=>{
        const user = response;
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.presence.createHubConnection(user);
          this.currentUserSource.next(user);
        }
      })
    )
  }

  register(model: any)
  {
    return this.http.post( this.baseUrl + "Accout/register", model).pipe(
      map( (response: User)=>{
        const user = response;
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.presence.createHubConnection(user);
          this.currentUserSource.next(user);
          return user;
        }
      })
    )
  }

  setCurrentUser(user: User)
  {
    this.currentUserSource.next(user);
  }


  logout()
  {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.presence.stopHubConnection();
  }
}
