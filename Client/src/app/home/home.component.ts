import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { erorr } from '../_models/erorr';


import { AccountService } from '../_services/account.service';

interface Gender {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  showForm = true;
  erorrRes! :  erorr;

  constructor(private router: Router, 
            private _snackBar: MatSnackBar,
    public accountService: AccountService, private fb: FormBuilder) { }
  hide = true;

  gender: Gender[] = [
    {value: "Both", viewValue: "Both"},
    {value: "Male", viewValue: "Male"},
    {value: "Female", viewValue: "Female"}
  ];

  myloginFrom = this.fb.group({
    username : ['', Validators.required],
    password : ['', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(8)])]
  });

  signup(){
    if(this.registerForm.valid){
      this.accountService.registration(this.registerForm.value).subscribe(res=>{

        this.router.navigateByUrl('/member');
        this.openSnackBar("Resiter successfully", this.registerForm.get('username')?.value);
      }, erorr=>{
        this.openSnackBar("Invalid User name or user name already exit", "Again Try");
      })
    }
  }

  onSubmit()
  {
    if(this.myloginFrom.valid){
      console.log(this.myloginFrom.value);
      this.accountService.login(this.myloginFrom.value).subscribe(res=>{
        console.log(res);
        this.router.navigateByUrl('/members');
        this.openSnackBar("Login-Succsfully", this.myloginFrom.get('username')?.value)
      }, erorr=>{
        this.erorrRes = erorr;
        console.log("Hi" + this.erorrRes.errors);
        this.openSnackBar("Wrong Username or Password", "Try Again");
      });
    }
  }

  email = new FormControl('', [Validators.required, Validators.email]);

  ngOnInit(): void {
    
    this.accountService.currentUser$.subscribe(res=>{
     
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
  
  
  registerForm = this.fb.group({
    username : ['', Validators.compose([Validators.required])],
    knownAs :  ['', Validators.compose([Validators.required])],
    dateOfBirth :  ['', Validators.compose([Validators.required, this.validateAge])],
    city :  ['', Validators.compose([Validators.required])],
    country:  ['', Validators.compose([Validators.required])],
    gender :  ['', Validators.compose([Validators.required])],
    password : ['', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(8)])],
    confirmPassword: ['', Validators.compose([Validators.required, this.checkPassword])],
  })
  
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

  getGender(event: any){
    return this.registerForm.get('gender');
  }
 
  registerToggle(){
    this.registerMode = !this.registerMode;
  }
  

  cancleRegisterMode(event: boolean){
    this.registerMode = event;
  }

  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }
}
