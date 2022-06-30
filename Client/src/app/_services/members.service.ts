import { HttpClient, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PageData } from '../_models/PageData';



@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getMembers(page: any){
    // return this.http.get<any[]>('https://localhost:7249/Users?' , page);
    let params: HttpParams = new HttpParams()
      .append('PageNumber', page.pageNumber)
      .append('PageSize', page.pageSize)
      .append('minAge', page.minAge)
      .append('maxAge', page.maxAge)
      .append('Gender', page.gender)
      .append('OrderedBy', page.orderby);
    return this.http.get<PageData>(
      'https://localhost:7249/Users?',
      {
        params: params,
      }
    );
  }

  getMember(username: string){
    return this.http.get<Member>(this.baseUrl + 'Users/' + username);
  }

  updateMember(member: any){
    return this.http.put('https://localhost:7249/Users', member, {});
  }

  setMainPhoto(photoId: string){
    return this.http.put('https://localhost:7249/Users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: string){
    return this.http.delete('https://localhost:7249/Users/delete-photo/' + photoId);
  }
}
