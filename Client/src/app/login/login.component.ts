import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email = new FormControl('', [Validators.required, Validators.email]);
  constructor( private fb: FormBuilder) { }

  loginForm! : FormGroup;

  ngOnInit(): void {
  }

  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }

  initializeForm()
  {
    this.loginForm = this.fb.group({
      username : ['', Validators.compose([Validators.required])],
      password : ['', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(8)])],
    })
  }

}
