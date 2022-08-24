import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

export function AgeValidator(
  control: AbstractControl
): { [key: string]: boolean } | null {
  if (control.value > 18) {
    
    return { age: true };
  }
  return null;
}

export function PasswordValidator(
  control: AbstractControl
): { [key: string]: boolean } | null {
  if (control.value) {
    
    return { age: true };
  }
  return null;
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model : any = {};

  @Input() sms : any;
  @Output() cancelRegister = new EventEmitter(); 

  constructor(private fb: FormBuilder, private accountService: AccountService, private toastr: ToastrService) { }
   
  profileForm = this.fb.group({
    userName: ['', [Validators.required, Validators.minLength(6)]],
    knownAs: ['', [Validators.required, Validators.minLength(4)]],
    dateOfBirth: ['', [Validators.required, this.validateAge]],
    gender: ['', [Validators.required]],
    city: ['', [Validators.required]],
    country: ['', [Validators.required]],
    password:['', [Validators.required, Validators.minLength(6)]],
    confirmpassword:['', [Validators.required, this.checkPassword]]
  });
  
  checkPassword(control: AbstractControl): ValidationErrors | null {
    if (control.value != control.parent?.get('password')?.value) return {rPassword:true}
    return null
  }

  validateAge(control: AbstractControl): ValidationErrors | null {
    let bd = control?.value
    let timeDef = Math.abs(Date.now()-new Date(bd).getTime())
    let age = Math.floor(timeDef/(1000*3600*24)/365)
    return age<18? {invalidAge:true}:null
  }

  

  onSubmit() {
    if(this.profileForm.valid){
      this.register();
    }
  }
  



  ngOnInit(): void {
    console.log(this.sms);
  }

  register()
  {
    this.accountService.register(this.profileForm.value).subscribe(res => {
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
