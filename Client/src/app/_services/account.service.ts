import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "https://localhost:7249/";
  private currentUserSource = new ReplaySubject<User | undefined>(1);
  
  currentUser$ = this.currentUserSource.asObservable();


  constructor(private http:HttpClient) { }

  login(model: any){
    return this.http.post( this.baseUrl + "Account/login", model).pipe(
        map((response : any)=>{
          // console.log(response);
          // console.log(model);
          // console.log("RES: " + response);
          const user = response.data;
          if(user){
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
          }
        }
      )
    )
  }


  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }


  logout()
  {
    localStorage.removeItem('user');
    this.currentUserSource.next(undefined);
  }


  registration(model: any){
    return this.http.post('https://localhost:7249/Account', model).pipe(
      map((res: any)=>{
        const user = res.data;
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  getAllUsers(){  
    return this.http.get('https://localhost:7249/Users');
  }
}
