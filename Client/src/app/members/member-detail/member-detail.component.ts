import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { member } from '../../_models/member';
import { MemberService } from '../../_services/member.service';
import { MessageService } from '../../_services/message.service';
import { message } from '../../_models/message';
import { PresenceService } from '../../_services/presence.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

  @ViewChild('memberTabs' ,{static: true}) memberTabs: TabsetComponent;

  member : member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  messages: message[] = [];
  activeTab: TabDirective;

  constructor(private memberService: MemberService, 
    private messageService: MessageService,
    private route: ActivatedRoute, 
    public presence: PresenceService) { }

  ngOnInit(): void {
    this.loadMember();
  }


  photosAdd()
  {
    this.galleryOptions = [
      {
        width: '100%',
        height: '600px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ];

    this.galleryImages = this.getImages();
  }


  getImages(): NgxGalleryImage[]{
    const imageUrls = [];
    console.log(this.member);
    for(const photo of this.member.photos)
    {
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    
    return imageUrls;
  }

  loadMember()
  {
    this.memberService.getMember(this.route.snapshot.paramMap.get('username')).subscribe(member =>{
      this.member = member;
      this.photosAdd();
    })
  }

  seletedTab(tabId: number)
  {
    this.memberTabs.tabs[tabId].active = true;
  }

  loadMessages()
  {
    this.messageService.getMessageThread(this.member.userName).subscribe(message => {
      this.messageService.messageThreadSource.next(message);
    })
  }

  onTabActivated(data: TabDirective)
  {
    console.log(data);
    this.activeTab = data;
    if(this.activeTab.heading === 'Message' && this.messages.length === 0)
    {
      this.loadMessages();
    }
  }
}
