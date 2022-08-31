import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { getPaginationHeaders, getPaginatedResult } from './paginationHelper';
import { message } from '../_models/message';
import { sendMessage } from '../_models/sendMessage';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, catchError, pipe, ReplaySubject, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  newMessage: message;

  public messageThreadSource = new BehaviorSubject<message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();


  constructor(private toastr: ToastrService, private http: HttpClient) { }

  createHubConnection(user: User, otherUsername: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'message?user=' + otherUsername, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build()

    this.hubConnection
      .start()
      .catch(error => console.log(error))


    this.hubConnection.on('NewMessage', message => {
      console.log("MY new message" + message);
      this.messageThread$.pipe(take(1)).subscribe(messages=>{
        this.messageThreadSource.next([...messages, message]);
      })
    })
  }


  stopHubConnection() {
    if (this.hubConnection) {
      this.messageThreadSource.next(null);
      this.hubConnection.stop();
    }
    // this.hubConnection.stop().catch(error => console.log(error));
  }

  getMessages(pageNumber, pageSize, container) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<message[]>(this.baseUrl + 'Messages', params, this.http);
  }

  getMessageThread(username: string) {
    return this.http.get<message[]>(this.baseUrl + 'Messages/thread/' + username);
  }


  sendMessage(username: string, content: string) {
    return this.http.post( this.baseUrl + "Messages", {
      recipientUsername: username,
      content
    });
  }
}
