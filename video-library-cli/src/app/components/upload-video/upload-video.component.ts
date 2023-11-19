import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { VideoService } from 'src/app/services/video.service';

@Component({
  selector: 'app-upload-video',
  templateUrl: './upload-video.component.html',
  styleUrls: ['./upload-video.component.scss']
})
export class UploadVideoComponent {
  requiredFileType!: string;
  fileName:string = '';
  uploadProgress!: number;
  uploadSub!: Subscription;
  
  constructor(
    private videoService: VideoService,
    private router: Router
  ) {}

  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      this.fileName = file.name;
      const formData = new FormData();
      formData.append(this.fileName, file);

      this.videoService.UploadVideo(this.fileName).subscribe((videoId: number) => {
        this.router.navigate(['view'], { queryParams: { id: videoId }});
      });
    }
  }
}
