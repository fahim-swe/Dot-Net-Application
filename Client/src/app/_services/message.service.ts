import { HttpClient, HttpParams } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, take } from 'rxjs';
import { messageParam } from '../_models/messageParam';
import { messageRes } from '../_models/messageRes';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private hubConnection!: HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);

  messageThread$ = this.messageThreadSource.asObservable();

  constructor( private http: HttpClient) { }

  createHubConnection(user: User, otherUsername: string)
  {
    this.hubConnection = new HubConnectionBuilder()
        .withUrl('https://localhost:7249/hubs/message?user=' + otherUsername , {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets,
          accessTokenFactory: ()=> user.token
        })
        .withAutomaticReconnect()
        .build()

    this.hubConnection.start().catch(error => console.log(error));

    this.hubConnection.on('ReceiveMessageThread', messages=>{
      this.messageThreadSource.next(messages);
    })

    this.hubConnection.on('NewMessage', message=>{
      // console.log("New message :--- " + message);
      this.messageThread$.pipe(take(1)).subscribe(messages => {
        console.log(message);
        this.messageThreadSource.next( [message, ...messages]);
      })
    })

  }

  stopHubConnection()
  {
    if(this.hubConnection){
      this.hubConnection.stop();
    }
    
  }

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

  async sentMessage(sms: any)
  {
    // console.log(sms);
    return this.hubConnection.invoke("SendMessage", sms)
          .catch(erorr=>{
            console.log("Error in sending sms: " + erorr);
          });

    // return this.http.post<messageRes>('https://localhost:7249/Messages', sms);
  }
}
