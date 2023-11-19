import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, ParamMap } from '@angular/router';
import { Video } from 'src/app/common/video';
import { VideoService } from 'src/app/services/video.service';
import { filter, switchMap } from 'rxjs/operators';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-view-video',
  templateUrl: './view-video.component.html',
  styleUrls: ['./view-video.component.scss']
})
export class ViewVideoComponent implements OnInit {
  video: Video = new Video();
  rating: number = 0;
  videoName: FormControl = new FormControl('');
  keywords: FormControl = new FormControl('');

  constructor(
    private route: ActivatedRoute,
    private videoService: VideoService,
    private snackbar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(params => {
      let videoId = Number(params.get('id'));
      this.videoService.Get(videoId).subscribe((video: Video) => {
        this.video = video;
        this.rating = video.rating;
        this.videoName.setValue(video.videoName);
        this.keywords.setValue(video.keywords);
      });
    });
  }

  ratingUpdated(rating: number): void {
    this.rating = rating;
  }

  saveClicked(): void {
    this.video.videoName = this.videoName.value;
    this.video.keywords = this.keywords.value;
    this.video.rating = this.rating;
    this.videoService.UpdateVideo(this.video).subscribe((val: any) => {
      this.snackbar.open('Video has been updated successfully', 'OK', {verticalPosition: 'top'});
    });
  }
}
