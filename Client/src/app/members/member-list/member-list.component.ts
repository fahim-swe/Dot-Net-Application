import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { member } from '../../_models/member';
import { MemberService } from '../../_services/member.service';
import { UserParams } from '../../_models/userParams';
import { User } from '../../_models/user';
import { AccountService } from '../../_services/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members : member[];
  username : string;
  pagination! : Pagination;
  userParams : UserParams;
  user: User;

  genderList = 
  [
    {value: 'male', display: 'Males'},
    {value: 'female', display: 'Female'}
  ];


  constructor (private accountService: AccountService, private memberService: MemberService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(user);
    })
   }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers()
  {
    this.memberService.getMembers(this.userParams).subscribe(res => {
      this.members = res.result;
      this.pagination = res.pagination;
    })
  }

  resetFilters()
  {
    this.userParams = new UserParams(this.user);
    this.loadMembers();
  }

  pageChanged(page: any)
  {
    console.log(page.page);
    this.userParams.pageNumber = page.page;
    this.loadMembers();
  }

}
