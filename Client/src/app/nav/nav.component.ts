import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../_models/User';
import { AccountService } from '../_services/account.service';

import {ToastrService} from 'ngx-toastr'

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}

  constructor(public accountService: AccountService, private router: Router,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {
    console.log("Hello");
    console.log(this.accountService.currentUser$ != null );
    
  }

  login(){
    // console.log(this.model);
    this.accountService.login(this.model).subscribe(response => {
      // console.log("Login: " + response);
      this.router.navigateByUrl('/members');
      
    }, error => {
      console.log("Error: " + error);
      this.toastr.error((error.messages));
    })
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
