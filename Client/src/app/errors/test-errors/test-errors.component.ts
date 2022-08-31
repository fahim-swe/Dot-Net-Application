import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {

  baseUrl = environment.apiUrl;

  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }

  get404Error()
  {
    this.http.get(this.baseUrl + "/buggy/not-found").subscribe(response =>{
      console.log(response);
    }, erorr=>{
      console.log(erorr);
    })
  }


  get500Error()
  {
    this.http.get(this.baseUrl + "/buggy/server-error").subscribe(response =>{
      console.log(response);
    }, erorr=>{
      console.log(erorr);
    })
  }

  get400Error()
  {
    

    this.http.get(this.baseUrl + "/buggy/bad-request", ).subscribe(response =>{
      console.log(response);
    }, erorr=>{
      console.log(erorr);
    })
  }

  get401Error()
  {
    this.http.get(this.baseUrl + "/buggy/auth").subscribe(response =>{
      console.log(response);
    }, erorr=>{
      console.log(erorr);
    })
  }


  get400ValidationError()
  {
    this.http.post(this.baseUrl + "/Accout/register", {}).subscribe(response =>{
      console.log(response);
    }, erorr=>{
      console.log(erorr);
    })
  }


}
