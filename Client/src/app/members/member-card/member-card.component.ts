import { Component, Input, OnInit } from '@angular/core';
import { member } from '../../_models/member';
import { PresenceService } from '../../_services/presence.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input() member:member;

  
  constructor(public presence: PresenceService) {
    
   }

  ngOnInit(): void {
  }

}
