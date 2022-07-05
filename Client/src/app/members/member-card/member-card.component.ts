import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

 
  @Input() Member!: Member;
  userName! : string;
  isOnline = false;

  constructor(public presence: PresenceService) { }

  ngOnInit(): void {
    
    this.presence.onlineUsers$.subscribe(res => {
      if(res.indexOf(this.Member.userName) !== -1){
        this.isOnline = true;
      }
    })
  }

}
