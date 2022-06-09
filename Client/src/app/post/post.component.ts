import { Component, OnInit } from '@angular/core';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  posts : any = {};
  constructor(private postService: PostService) { }

  ngOnInit(): void {
    this.getAllPost();
  }

  getAllPost()
  {
    this.postService.getUserPost().subscribe(response=>{
      this.posts = response;
      console.log(this.posts);
    });
  }

}
