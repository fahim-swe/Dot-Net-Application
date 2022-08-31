import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { member } from '../_models/member';
import { map } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';




// const headers = new HttpHeaders({
   
//   'Authorization': 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token,
//   'Content-Type':'application/json; charset=utf-8'
// });

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  baseUrl = environment.apiUrl;
  members: member[] = [];


  constructor(private http: HttpClient) { }

  getMembers(userParams: UserParams)
  {
    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge.toString());
    params = params.append('maxAge', userParams.maxAge.toString());
    params = params.append('gender', userParams.gender.toString());

    return getPaginatedResult<member[]>( this.baseUrl+'Users' , params, this.http);
    
  }


  getMember(username: string)
  {
    return this.http.get<member>(this.baseUrl + 'Users/' + username);
  }

  
}
