import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';


@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getMembers(){
    return this.http.get<Member[]>(this.baseUrl + 'Users');
  }

  getMember(username: string){
    return this.http.get<Member>(this.baseUrl + 'Users/' + username);
  }

  updateMember(member: any){
    return this.http.put('https://localhost:7249/Users', member);
  }


  setMainPhoto(photoId: string){
    return this.http.put( 'https://localhost:7249/Users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: string){
    return this.http.delete('https://localhost:7249/Users/delete-photo/' + photoId);
  }
}
