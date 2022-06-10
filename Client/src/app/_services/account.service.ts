import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "https://localhost:7249";
  private currentUserSource = new ReplaySubject<User | null>(1);
  
  currentUser$ = this.currentUserSource.asObservable();


  constructor(private http:HttpClient) { }

  login(model: any){
    return this.http.post(this.baseUrl+"/Account/login", model).pipe(
        map((response : any)=>{

          console.log(model);
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
    this.currentUserSource.next(null);
  }


  registration(model: any){
    return this.http.post(this.baseUrl+"/Account", model);
  }

  getAllUsers(){  
    return this.http.get(this.baseUrl + "/Users");
  }
}
