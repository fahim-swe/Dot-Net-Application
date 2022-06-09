import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  users: any;
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.getAllUsers();
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  getAllUsers(){
    this.accountService.getAllUsers().subscribe(users => this.users = users);
  }

  cancleRegisterMode(event: boolean){
    this.registerMode = event;
  }
}
