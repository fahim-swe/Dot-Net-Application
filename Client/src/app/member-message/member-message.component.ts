import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { messageRes } from '../_models/messageRes';
import { MessageService } from '../_services/message.service';



@Component({
  selector: 'app-member-message',
  templateUrl: './member-message.component.html',
  styleUrls: ['./member-message.component.css']
})
export class MemberMessageComponent implements OnInit {

  @Input() username!: string;
  messageRes!: messageRes[];
  sms!: messageRes;
  
  myContent! : FormGroup;
  


  constructor(private fb: FormBuilder, private messageService: MessageService) { }

  
  
 
  
  ngOnInit(): void {
    this.getUserMessage(this.username);
    this.Ini();
  }

  Ini()
  {
    this.myContent = this.fb.group({
      recipientUsername : [this.username, Validators.compose([Validators.required])],
      content : ['', Validators.compose([Validators.minLength(2)])]
    })
  }

  getUserMessage(username: string){
    this.messageService.getMessageFromUser(username).subscribe(res=>{
      this.messageRes = res;
    })
  }

  sendSMS(){
    if(this.myContent.valid){
      console.log(this.myContent);
      this.messageService.sentMessage(this.myContent.value).subscribe(res=>{
        console.log(res);
        this.sms = res;
        this.myContent.reset();
        this.messageRes.unshift(this.sms);
        
      })
    }
    
  }

}
