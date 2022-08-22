import { Component, OnInit } from '@angular/core';
import { member } from '../../_models/member';
import { MemberService } from '../../_services/member.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members : member[];

  constructor(private memberService: MemberService) { }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers()
  {
    this.memberService.getMembers().subscribe(res => {
      this.members = res;
      console.log(res);
    })
  }

}
