import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import { VideoListComponent } from './components/video-list/video-list.component';
import { ViewVideoComponent } from './components/view-video/view-video.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StarRatingComponent } from './components/star-rating/star-rating.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { UploadVideoComponent } from './components/upload-video/upload-video.component';
import { MatProgressBarModule } from '@angular/material/progress-bar';

const appRoutes: Routes = [
  { path: '', component: VideoListComponent },
  { path: 'view', component: ViewVideoComponent },
  { path: 'upload', component: UploadVideoComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    VideoListComponent,
    ViewVideoComponent,
    StarRatingComponent,
    UploadVideoComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    MatCardModule,
    MatPaginatorModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    CommonModule,
    MatButtonModule,
    BrowserAnimationsModule,
    MatTooltipModule,
    MatIconModule,
    MatSnackBarModule,
    MatProgressBarModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
