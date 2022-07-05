import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  
  hubUrl = environment.hubUrl;
  private hubConnection!: HubConnection;
  private onlineUsersSource = new BehaviorSubject<string[]>([]);
  onlineUsers$ = this.onlineUsersSource.asObservable();


  constructor(private _snackBar: MatSnackBar) { }


  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }


  createHubConnection(user : User)
  {
    this.hubConnection = new HubConnectionBuilder()
        .withUrl('https://localhost:7249/hubs/presence', {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets,
          accessTokenFactory: ()=> user.token
        })
        .withAutomaticReconnect()
        .build()

    this.hubConnection
        .start()
        .catch(error => {
          console.log(error)
        })

    
    
    this.hubConnection.on('UserIsOnline', username => {
      console.log("has coneected " + username);
      this.openSnackBar("Has connected", username);
    })

    this.hubConnection.on('UserIsOffline', username=>{

      this.openSnackBar("has disconnected", username);
    })

    this.hubConnection.on('GetOnlineUsers', (username: string[])=>{
      this.onlineUsersSource.next(username);
      // console.log(username);
    })
  }


  stopHubConnection()
  {
    this.hubConnection.stop().catch(error => console.log(error));
  }
}
