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

  messageRes!: messageRes[];
  @Input() username!: string;
  sms!: messageRes;
  
  myContent! : FormGroup;
  


  constructor(private fb: FormBuilder, public messageService: MessageService) {
   
   }

  
  
 
  
  ngOnInit(): void {
    this.getUserMessage();
    this.Ini();
  }

  Ini()
  {
    this.myContent = this.fb.group({
      recipientUsername : [this.username, Validators.compose([Validators.required])],
      content : ['', Validators.compose([Validators.minLength(2)])]
    })
  }

 

  sendSMS(){
    console.log(this.myContent.value);
    if(this.myContent.valid){
      
      this.messageService.sentMessage(this.myContent.value).then((message)=>{
        this.myContent.reset();
        this.Ini();
      })
    }
  }


  getUserMessage(){
    // this.messageService.messageThread$.pipe().subscribe(res => {
    //   this.messageRes = res;
    // })
    this.messageService.messageThread$.subscribe(res => {
      this.messageRes = res as unknown as messageRes[];
    })
  }

}
