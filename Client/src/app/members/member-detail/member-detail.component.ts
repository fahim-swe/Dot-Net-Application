import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { member } from '../../_models/member';
import { MemberService } from '../../_services/member.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

  member : member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private memberService: MemberService, private route: ActivatedRoute) { }

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
}
