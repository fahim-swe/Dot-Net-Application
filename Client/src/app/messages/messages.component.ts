import { Component, OnInit } from '@angular/core';
import { messageParam } from '../_models/messageParam';
import { messageRes } from '../_models/messageRes';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messageParam : messageParam = {
    PageNumber: 1,
    PageSize: 10,
    minAge: 10,
    maxAge: 150,
    Gender: 0,
    OrderedBy: 0,
    Container: "Unread"
  } as messageParam;

  messageResponse!: messageRes[];

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.getallMessage();
  }

  unreadSMS()
  {
    this.messageParam.Container = "Unread";
    this.getallMessage();
  }
  outboxSMS()
  {
    this.messageParam.Container = "Outbox";
    this.getallMessage();
  }
  inboxSMS()
  {
    this.messageParam.Container = "Inbox";
    this.getallMessage();
  }
  getallMessage(){
    this.messageService.getMessage(this.messageParam).subscribe(res=>{
      this.messageResponse = res;
      console.log(this.messageResponse);
    })
  }

}
