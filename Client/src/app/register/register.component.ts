import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';



import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Input() usersFromHomeComponent: any;
  
  @Output() cancelRegister = new EventEmitter<boolean>();
  value = 'Clear me';
  model : any = {};
  registerForm! : FormGroup;
  hide = true;


  errorMessage = "";

  constructor(private accountService: AccountService, 
    private fb: FormBuilder, private router: Router,
    private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.initializeForm();
    
  }
  
  initializeForm()
  {
    this.registerForm = this.fb.group({

      username : ['', Validators.compose([Validators.required])],
      knownAs :  ['', Validators.compose([Validators.required])],
      dateOfBirth :  ['', Validators.compose([Validators.required, this.validateAge])],
      city :  ['', Validators.compose([Validators.required])],
      country:  ['', Validators.compose([Validators.required])],
      gender :  ['', Validators.compose([Validators.required])],
      password : ['', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(8)])],
      confirmPassword: ['', Validators.compose([Validators.required, this.checkPassword])],
    })
  }

  validateAge(control: AbstractControl): ValidationErrors | null {
    let bd = control?.value
    let timeDef = Math.abs(Date.now()-new Date(bd).getTime())
    let age = Math.floor(timeDef/(1000*3600*24)/365)
    return age<18? {invalidAge:true}:null
  }

  checkPassword(control: AbstractControl): ValidationErrors | null {
    if (control.value != control.parent?.get('password')?.value) return {rPassword:true}
    return null
  }


  registerUser()
  {
    if(this.registerForm.valid){
      console.log(this.registerForm.value);
      this.accountService.registration(this.registerForm.value).subscribe(res => {
        this.router.navigateByUrl('/members');
        this.openSnackBar("Register User", "Successfully")
      }, error =>{
        this.errorMessage = error.data;
        this.openSnackBar("User Name already Taken", "Error");
        console.log(error);
      })
    }
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
  
  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }
  
}

