import { Component, Input, OnInit } from '@angular/core';

import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { environment } from 'src/environments/environment';


const URL = environment.apiUrl;

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input() member!: Member;
  uploader!: FileUploader;
  hasBaseDropZoneOver:boolean | undefined;
  hasAnotherDropZoneOver:boolean | undefined;
  response:string | undefined;

  user : any;

  baseUrl = environment.apiUrl;

constructor(private accountService: AccountService, private memberService: MembersService) {
    accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);

    
    this.uploader = new FileUploader({
        url: this.baseUrl+ 'Users/add-photo',
        authToken: 'Bearer ' + this.user.token,
        isHTML5: true,
        allowedFileType: ['image'],
        autoUpload: false,
      });

      this.uploader.onAfterAddingFile= (file)=>{
        file.withCredentials = false
      }
   
      this.hasBaseDropZoneOver = false;
      this.hasAnotherDropZoneOver = false;
   
      this.response = '';
   
      this.uploader.response.subscribe( res => {
        
        const photos = JSON.parse(res);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain
        };
      });

      


      this.uploader.onSuccessItem = (item, response, status, headers) => {
        if (response) {
          const res: Photo = JSON.parse(response);
          const photo = {
            id: res.id,
            url: res.url,

            isMain: res.isMain
          };
        
          this.member.photos.push(res);

        }
      }
  }



  ngOnInit(): void { 
   
  }


  


  public fileOverBase(e:any):void {
    this.hasBaseDropZoneOver = e;
  }
 
  public fileOverAnother(e:any):void {
    this.hasAnotherDropZoneOver = e;
  }


  deletePhoto(photo: Photo)
  {
    this.memberService.deletePhoto(photo.id).subscribe( ()=>{
      
      this.member.photos.forEach( (element, index)=>{
        if(element.id == photo.id) this.member.photos.splice(index, 1);
      })
    })
  }

  setMainPhoto(photo: Photo){
    console.log(photo);
    this.memberService.setMainPhoto(photo.id).subscribe( ()=>{
      this.user.photoUrl = photo.url;
      this.accountService.setCurrentUser(this.user);
      this.member.photoUrl = photo.url;

      this.member.photos.forEach( p=>{
        if(p.isMain) p.isMain = false;
        if(p.id === photo.id) p.isMain = true;
      })
    })
  }

}
