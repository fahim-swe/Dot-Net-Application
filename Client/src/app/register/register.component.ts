import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Input() usersFromHomeComponent: any;
  
  @Output() cancelRegister = new EventEmitter<boolean>();

  model : any = {};

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  registerUser()
  {
    this.accountService.registration(this.model).subscribe(response=>{
      this.cancel();
      console.log(response);
    }, error=>{
      console.log(error);
    })
    console.log(this.model);
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
