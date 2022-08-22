import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { member } from '../_models/member';




// const headers = new HttpHeaders({
   
//   'Authorization': 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token,
//   'Content-Type':'application/json; charset=utf-8'
// });

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMembers()
  {
    return this.http.get<member[]>(this.baseUrl + 'Users');
  }

  getMember(username: string)
  {
    return this.http.get<member>(this.baseUrl + 'Users/' + username);
  }
}
