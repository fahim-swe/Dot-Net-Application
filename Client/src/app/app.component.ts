import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';
import { PresenceService } from './_services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Client';

  users: any;
  constructor(private accountService: AccountService, private presence: PresenceService)
  {

  }
  ngOnInit(): void {

    this.setCurrentUser();
  }

  setCurrentUser()
  {
    const user: User = JSON.parse(localStorage.getItem('user'));
    if(user){
      this.presence.createHubConnection(user);
    }
    this.accountService.setCurrentUser(user);
  }
}
