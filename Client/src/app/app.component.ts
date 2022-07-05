import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/User';
import { AccountService } from './_services/account.service';
import { PresenceService } from './_services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  users : any = {};
  title = 'Client';
  

  constructor(private http: HttpClient, 
    private presence : PresenceService,
    private accountService: AccountService){}
  ngOnInit(): void {
    this.setCurrentUser();
  }


  setCurrentUser()
  {
    const user: User = JSON.parse(localStorage.getItem('user') || '{}');
    if(user){
      this.accountService.setCurrentUser(user);
      this.presence.createHubConnection(user);
    }
    
  }
  
}
