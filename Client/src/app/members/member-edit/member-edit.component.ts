import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/user';
import { member } from '../../_models/member';
import { AccountService } from '../../_services/account.service';
import { MemberService } from '../../_services/member.service';
import { take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  @ViewChild('editForm') editForm: NgForm;
  member: member;
  user: User;
  constructor(private accoutnService: AccountService, 
    private toast: ToastrService,
    private memberService: MemberService) {
    this.accoutnService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
    })
   }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember()
  {
    this.memberService.getMember(this.user.username).subscribe(member =>{
      this.member = member;
      console.log(this.member); 
    })
  }

  updateProfile()
  {
    console.log(this.member);
    this.toast.success("profile updated successfully");
    this.editForm.reset(this.member);
  }

}
