import { AfterContentInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { message } from '../../_models/message';
import { MessageService } from '../../_services/message.service';
import { sendMessage } from '../../_models/sendMessage';
import { NgForm } from '@angular/forms';
import { AccountService } from '../../_services/account.service';
import { take } from 'rxjs';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-member-message',
  templateUrl: './member-message.component.html',
  styleUrls: ['./member-message.component.css']
})
export class MemberMessageComponent implements OnInit, AfterContentInit {

  @ViewChild('messageForm') messageForm: NgForm;

  @Input() username: string;
  messgeContent: string;
  container: HTMLElement;     



  constructor(public messageService: MessageService) { }
  

  ngOnInit(): void {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.messageService.createHubConnection(user, this.username);
    this.messageService.getMessageThread(this.username).subscribe(message => {
      this.messageService.messageThreadSource.next(message);
    })  

    console.log(this.messageService.messageThread$);
  }



  sendMessage()
  {
    this.messageService.sendMessage(this.username, this.messgeContent).subscribe(message => {
      this.messageForm.reset();
    });
  }

  ngAfterContentInit(): void {
    this.container = document.getElementById("msgContainer");           
    this.container.scrollTop = this.container.scrollHeight;   
  }
}
