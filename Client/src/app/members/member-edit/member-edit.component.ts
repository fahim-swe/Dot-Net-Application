import { Component, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/User';
import { MembersService } from 'src/app/_services/members.service';
import {AccountService} from 'src/app/_services/account.service';
import { take } from 'rxjs';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  @ViewChild('editForm') editForm!: NgForm;
  Member: any;
  user : any;

  constructor(private accountService: AccountService, private memberService: MembersService
    ,private toastr : ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe( user => this.user = user);
   }

  ngOnInit(): void {
    this.loadMember();
  }


  loadMember()
  {
    this.memberService.getMember(this.user.username).subscribe(member => {
      this.Member = member;
      // console.log(this.member);
    });
  }

  updateMember(){
    this.memberService.updateMember(this.Member).subscribe(()=>{
      console.log(this.Member);
      this.toastr.success("Profile updated successfully");
      this.editForm.reset(this.Member);
    });
    
  }

}
