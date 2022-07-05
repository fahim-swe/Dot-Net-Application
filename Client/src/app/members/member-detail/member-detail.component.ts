import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatTabChangeEvent, MatTabGroup } from '@angular/material/tabs';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { NgxGalleryAnimation } from '@kolkov/ngx-gallery';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { messageRes } from 'src/app/_models/messageRes';
import { User } from 'src/app/_models/User';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { MessageService } from 'src/app/_services/message.service';
import { PresenceService } from 'src/app/_services/presence.service';


@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']


})
export class MemberDetailComponent implements OnInit, OnDestroy {

  @ViewChild(MatTabGroup, {static: true}) tabGroup!: MatTabGroup;

  Member!: Member;
  user : any;
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];

  Messages: messageRes[] = [];




  constructor(
    private accountService: AccountService,
    private messageService: MessageService,
    public presence: PresenceService,
    private router: Router,
    private memberService: MembersService, private route : ActivatedRoute) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe(user =>{
        this.user = user;
      });
    }
  

  ngOnInit(): void {
    this.loadMember();

    this.route.queryParams.subscribe((data) => {
      if(data['index'] && (data['index'] === '0' || data['index']=== '1')) {
        this.tabGroup.selectedIndex = data['index'];
      } else {
        this.router.navigate(
          [], 
          {
            relativeTo: this.route,
            queryParams: { index: '0' },
            queryParamsHandling: 'merge'
        });
      }
    });
    this.galleryOptions = [
      {
        width: '90%',
        height: '500px',
        imagePercent : 100,
        thumbnailsColumns : 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ]
  }


  tabChanged(tabChangeEvent: MatTabChangeEvent){
    if(tabChangeEvent.index == 3){
      this.messageService.createHubConnection(this.user, this.Member.userName);
    }else{
      this.messageService.stopHubConnection();
    }
  }

  loadMember()
  {
    const username = this.route.snapshot.paramMap.get('username') || 'yourDefaultString';
    this.memberService.getMember(username).subscribe(member => {
      this.Member = member;
      this.galleryImages = this.getImages();
      // console.log(this.galleryImages);
    })

  }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];

    for(const photo of this.Member.photos){
      imageUrls.push({
        small : photo.url,
        medium : photo.url,
        big : photo.url
      })
    }

    return imageUrls;
  }



  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }

}
