import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model : any = {};

  @Input() sms : any;
  @Output() cancelRegister = new EventEmitter(); 

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
    console.log(this.sms);
  }

  register()
  {
    this.accountService.register(this.model).subscribe(res => {
      console.log(res);
      this.toastr.success("Register Successfully");
      this.cancel();
    })
  }

  cancel()
  {
    this.cancelRegister.emit(false);
  }
}
