import { Component, Input, OnInit } from '@angular/core';
import { member } from '../../_models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input() member:member;
  
  constructor() { }

  ngOnInit(): void {
  }

}
