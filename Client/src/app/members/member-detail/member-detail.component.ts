import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { NgxGalleryAnimation } from '@kolkov/ngx-gallery';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';


@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']


})
export class MemberDetailComponent implements OnInit {

  Member!: Member;
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];

  constructor(private memberService: MembersService, private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();

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



  loadMember()
  {
    const username = this.route.snapshot.paramMap.get('username') || 'yourDefaultString';
    this.memberService.getMember(username).subscribe(member => {
      this.Member = member;
      this.galleryImages = this.getImages();
      console.log(this.galleryImages);
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

}
