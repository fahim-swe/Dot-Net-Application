import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { messageParam } from '../_models/messageParam';
import { messageRes } from '../_models/messageRes';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http: HttpClient) { }

  getMessage(
    param: messageParam
  )
  {
    let params: HttpParams = new HttpParams()
    .append('PageNumber', param.PageNumber)
    .append('PageSize', param.PageSize)
    .append('minAge', param.minAge)
    .append('maxAge', param.maxAge)
    .append('Gender', param.Gender)
    .append('OrderedBy', param.OrderedBy)
    .append('Container', param.Container);


  return this.http.get<messageRes[]>('https://localhost:7249/Messages',
    {
      params: params,
    }
  );
  }

  
  getMessageFromUser(username: string){
    return this.http.get<messageRes[]>('https://localhost:7249/Messages/thread/' + username);
  }

  sentMessage(sms: any)
  {
    return this.http.post<messageRes>('https://localhost:7249/Messages', sms);
  }
}
