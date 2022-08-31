import { Component, OnInit } from '@angular/core';
import { message } from '../_models/message';
import { Pagination } from '../_models/pagination';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: message[];
  pagination : Pagination;
  container = 'Unread';
  pageNumber = 1;
  pageSize = 5;

  loading: boolean;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages()
  {
    this.loading = false;
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.container).subscribe(response => {
      this.messages = response.result;
      this.pagination = response.pagination;
      this.loading = true;
    })
  }

  pageChanged(event: any)
  {
    if(this.pageNumber != event.page)
    {
      this.pageNumber = event.page;
      this.loadMessages(); 
    }
  }
  
}
