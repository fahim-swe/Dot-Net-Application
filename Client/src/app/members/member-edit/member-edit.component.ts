import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/User';
import { MembersService } from 'src/app/_services/members.service';
import {AccountService} from 'src/app/_services/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  member: any;
  user : any;

  constructor(private accountService: AccountService, private memberService: MembersService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe( user => this.user = user);
   }

  ngOnInit(): void {
    this.loadMember();
  }


  loadMember()
  {
    this.memberService.getMember(this.user.username).subscribe(member => {
      this.member = member;
      // console.log(this.member);
    });
  }

}
