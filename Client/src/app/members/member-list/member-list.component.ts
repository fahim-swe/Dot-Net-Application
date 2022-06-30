import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';

import { Member } from 'src/app/_models/member';
import { Page } from 'src/app/_models/Page';
import { PageData } from 'src/app/_models/PageData';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

interface AGE {
  value: number;
  viewValue: string;
}

interface Gender {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members! :  Member[];
  totalRecord! : number;
  pageResponse! : PageData;
  showUi =  false;
  user:any;

  page : Page = {
    pageNumber: 0,
    pageSize: 10,
    minAge: 10,
    maxAge: 150,
    gender: 0,
    orderby: 0
  } as Page;

  ageFrom: AGE[] = [
    {value: 10, viewValue: '10'},
    {value: 15, viewValue: '15'},
    {value: 20, viewValue: '20'},
    {value: 25, viewValue: '25'},
    {value: 30, viewValue: '30'},
    {value: 35, viewValue: '35'},
    {value: 40, viewValue: '40'}
  ];
  

  ageTo: AGE[] = [
    {value: 10, viewValue: '10'},
    {value: 15, viewValue: '15'},
    {value: 20, viewValue: '20'},
    {value: 25, viewValue: '25'},
    {value: 30, viewValue: '30'},
    {value: 35, viewValue: '35'},
    {value: 40, viewValue: '40'}
  ];
 
  gerder: Gender[] = [
    {value: 0, viewValue: "Both"},
    {value: 1, viewValue: "Male"},
    {value: 2, viewValue: "Female"}
  ];

  constructor(private accountService: AccountService, 
    private route: Router,
    private memberService: MembersService) { }

  ngOnInit(): void {
   
    this.loadMembers();
    this.accountService.currentUser$.subscribe(res=>{
     
      if(res?.username == null){
        this.showUi = false;
        this.route.navigateByUrl('/');
      }
      else{
        this.showUi = true;
      }
      
    })  
    console.log(this.showUi);
  }

  loadMembers(){
    console.log(this.page);
    this.memberService.getMembers(this.page).subscribe (res=> {
        this.pageResponse = res;
        this.members = this.pageResponse.data;
        console.log(this.pageResponse);
    })
  }


  pageEvent(event: PageEvent){
    this.page.pageSize = event.pageSize;
    this.page.pageNumber = event.pageIndex+1;
    console.log(this.page);
    console.log(event);

    this.loadMembers();
  }

  applyFiler(){
    console.log(this.page);
    this.loadMembers();
  }

  lastActive(){
    this.page.orderby = 2;
    this.loadMembers();
  }

  recentyJoin(){
    this.page.orderby = 1;
    this.loadMembers();
  }


  getFromAge(value: any){
    this.page.minAge = value;
  }

  getToAge(value: any){
    this.page.maxAge = value;
  }

  getGender(value: any){
    this.page.gender = value;
  }
}
