import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { Video } from 'src/app/common/video';
import { VideoService } from 'src/app/services/video.service';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-video-list',
  templateUrl: './video-list.component.html',
  styleUrls: ['./video-list.component.scss']
})
export class VideoListComponent implements OnInit {
  videoList: Video[] = [];
  videoCount!: number;
  videosPerPage: number = 25;
  currentPage: number = 0;
  keywords: FormControl = new FormControl('');
  timeoutId!: any;
  imageCount: number = 0;
  video!: Video;

  constructor(
    private videoService: VideoService,
    private router: Router,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.currentPage = Number(sessionStorage.getItem('currentPage')) || 0;
    let keywords = sessionStorage.getItem('keywords');
    this.keywords.setValue(keywords);
    if (keywords != null && keywords.length > 0) {
      this.videoService.GetSearchVideoCount(this.keywords.value).subscribe((val: number) => {
        this.videoCount = val;
      });
  
      this.videoService.GetPagedSearch(this.keywords.value, 0, this.videosPerPage).subscribe((videoList: Video[]) => {
        this.videoList = videoList;
      });
    } else {
      this.videoService.GetVideoCount().subscribe((count: number) => {
        this.videoCount = count;
      });
  
      let start = (this.currentPage) * this.videosPerPage
      this.videoService.GetPaged(start, this.videosPerPage).subscribe((videoList: Video[]) => { 
        this.videoList = videoList; 
      });
    }
  }

  imageOnClick(videoId: number) : void {
    this.router.navigate(['view'], { queryParams: { id: videoId }});
  }

  onPage(event: PageEvent): void {
    this.currentPage = event.pageIndex;
    sessionStorage.setItem('currentPage', this.currentPage.toString());
    let start = (event.pageIndex) * this.videosPerPage
    if(this.keywords.value == null || this.keywords.value.length == 0 || this.keywords.value.length == undefined) {
      this.videoService.GetPaged(start, this.videosPerPage).subscribe((videoList: Video[]) => { 
        this.videoList = videoList; 
      });  
    } else {
      this.videoService.GetPagedSearch(this.keywords.value, start, this.videosPerPage).subscribe((videoList: Video[]) => {
        this.videoList = videoList;
      })
    }
  }

  onSearch() {
    this.currentPage = 0;
    sessionStorage.setItem('keywords', this.keywords.value);
    if(this.keywords.value.length == 0 || this.keywords.value.length == undefined) {
      this.videoService.GetVideoCount().subscribe((count: number) => {
        this.videoCount = count;
      });
  
      let start = (this.currentPage) * this.videosPerPage
      this.videoService.GetPaged(start, this.videosPerPage).subscribe((videoList: Video[]) => { 
        this.videoList = videoList; 
      });
    } else {
      this.videoService.GetSearchVideoCount(this.keywords.value).subscribe((val: number) => {
        this.videoCount = val;
      });
  
      this.videoService.GetPagedSearch(this.keywords.value, 0, this.videosPerPage).subscribe((videoList: Video[]) => {
        this.videoList = videoList;
      });
    }
  }

  ratingUpdated(videoId: number, rating: number): void {
    let video = this.videoList.find(v => v.videoId == videoId);
    if(video == undefined) {
      return;
    }
    video.rating = rating;
    this.videoService.UpdateVideo(video).subscribe((val: any) => {});
  }

  onMouseOver(videoId: number): void {
    this.video = this.videoList.find((v: Video) => v.videoId == videoId) || new Video();
    let matches = this.video.thumbnailPath.match(/.*(\d\d\d).png/); 
    if(matches == null) {
      return;
    }
    let ctString = matches[1];
    this.imageCount = Number(ctString);
    this.timeoutId = setTimeout(() => this.changeImage(), 1000);
  }

  changeImage(): void {
    if(++this.imageCount > 10) this.imageCount = 0;
    let paddedNumber = ('000' + this.imageCount).slice(-3);
    let newThumbnail = this.video.thumbnailPath.replace(/(.*)(\d\d\d)(\.png)/, `$1${paddedNumber}$3`);
    this.http.get(newThumbnail)
    .pipe(catchError((err: any, caught: Observable<any>): Observable<any> => { 
      if(err.status == 200) {
        this.video.thumbnailPath = newThumbnail; 
        this.timeoutId = setTimeout(() => this.changeImage(), 1000);
      } else {
        this.imageCount = 0; 
        this.changeImage();
      }
      return of();
    }))
    .subscribe((data: any) => {
      this.video.thumbnailPath = newThumbnail; 
      this.timeoutId = setTimeout(() => this.changeImage(), 1000);
    });
  }
  
  onMouseOut(videoId: number): void {
    clearTimeout(this.timeoutId);
    let newThumbnail = this.video.thumbnailPath.replace(/(.*)(\d\d\d)(\.png)/, `$1001$3`);
    this.video.thumbnailPath = newThumbnail;
    this.imageCount = 0;
  }
}
