import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  baseUrl = "https://localhost:7249";
  constructor(private http: HttpClient) { }

  getUserPost()
  {
    return this.http.get(this.baseUrl+"/Post");
  }
}
