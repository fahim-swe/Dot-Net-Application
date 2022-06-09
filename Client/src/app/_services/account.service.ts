import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "https://localhost:7249";
  constructor(private http:HttpClient) { }

  login(model: any){
    return this.http.post(this.baseUrl+"/Account/login", model);
  }

  registration(model: any){
    return this.http.post(this.baseUrl+"/Account", model);
  }

  getAllUsers(){  
    return this.http.get(this.baseUrl + "/Users");
  }
}
