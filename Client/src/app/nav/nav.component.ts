import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AccountService } from '../_services/account.service';

import {ToastrService} from 'ngx-toastr'
import { FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  showForm = false;
  user : any;

  constructor(public accountService: AccountService, private router: Router,
    private _snackBar: MatSnackBar
    ) { }

  ngOnInit(): void {
    // console.log("Hello");
    // console.log(this.accountService.currentUser$);
  
    this.accountService.currentUser$.subscribe(res=>{
     
      this.user = res;
      // console.log("HI" + res?.photoUrl );
      if(res?.username == undefined){
        this.showForm = true;
      }
      else{
        this.showForm= false;
      }

      // console.log(this.showForm);
      
    })
  }

  login(){
    // console.log(this.model);
    this.accountService.login(this.model).subscribe(response => {
      // console.log("Login: " + response);
      this.router.navigateByUrl('/members');
      
    }, error => {
      console.log("Error: " + error);
      
    })
  }

  logout(){
    this.openSnackBar("Login out successfully", this.user.username);
    this.accountService.logout();
    this.router.navigateByUrl('');
   
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }
}
